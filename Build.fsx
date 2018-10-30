#r "paket: groupref FakeBuild //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.IO
open Fake.BuildServer
open Fake.IO.Globbing.Operators
open Fake.DotNet
open Fake.DotNet.NuGet
open Fake.Core
open Fake.Tools
open Fake.Api
open Octokit
open Octokit.Clients

BuildServer.install [
    AppVeyor.Installer
]

let isAppveyor = AppVeyor.detect()
let gitVersion = GitVersion.generateProperties id

Target.create "Clean" (fun _ ->
  ["reports" ; "src/common" ; "nuget/lib"]
  |> Seq.iter Directory.delete

  !! "nuget/*"
  -- "nuget/*.txt"
  -- "nuget/*.nuspec"
  |> File.deleteAll

  let configuration = 
    (fun p -> { p with 
                  Properties = ["Configuration", "Release"]
                  Verbosity = Some MSBuildVerbosity.Minimal })

  !! "src/BCC.Core.sln"
  |> MSBuild.run configuration null "Clean" list.Empty
  |> Trace.logItems "Clean-Output: "
)

Target.create "Build" (fun _ ->
  CreateProcess.fromRawWindowsCommandLine "gitversion" "/updateassemblyinfo src\common\SharedAssemblyInfo.cs /ensureassemblyinfo"
  |> Proc.run
  |> ignore

  let configuration = (fun p -> { p with 
                                    DoRestore = true
                                    Verbosity = Some MSBuildVerbosity.Minimal })

  !! "src/BCC.Core.sln"
  |> MSBuild.runRelease configuration null "Build"
  |> Trace.logItems "AppBuild-Output: "
)

Target.create "Test" (fun _ ->
    List.allPairs ["BCC.Core.Tests"] ["net471" ; "netcoreapp2.1"]
    |> Seq.iter (fun (proj, framework) -> 
            let projectPath = sprintf "src\\%s\\%s.csproj" proj proj
            let reportFile = sprintf "%s-%s.results.trx" proj framework

            let configuration: (DotNet.TestOptions -> DotNet.TestOptions)
                = (fun t -> {t with
                               Configuration = DotNet.BuildConfiguration.Release
                               NoBuild = true
                               Framework = Some framework
                               Logger = Some (sprintf "trx;LogFileName=%s" reportFile)
                               ResultsDirectory = Some "../../reports"})

            DotNet.test configuration projectPath
            
            Trace.publish ImportData.BuildArtifact (sprintf "reports/%s" reportFile)
    )
)

Target.create "Package" (fun _ ->
    Shell.mkdir "nuget"
    
    !! "Package.nuspec"
    |> Shell.copy "nuget"

    Shell.copyRecursive "src/BCC.Core/bin/Release" "nuget/lib" false
    |> ignore

    NuGet.NuGetPack (fun p -> { p with
                                  Version = gitVersion.NuGetVersionV2
                                  OutputPath = "nuget" }) "nuget/Package.nuspec"

    !! "nuget/*.nupkg"
    |> Seq.iter (Trace.publish ImportData.BuildArtifact)
)

Target.create "Coverage" (fun _ ->
    List.allPairs ["BCC.Core.Tests"] ["net471" ; "netcoreapp2.1"]
    |> Seq.iter (fun (proj, framework) -> 
            let dllPath = sprintf "src\\%s\\bin\\Release\\%s\\%s.dll" proj framework proj
            let projectPath = sprintf "src\\%s\\%s.csproj" proj proj
            let reportPath = sprintf "reports/%s-%s.coverage.xml" proj framework

            sprintf "%s --target \"dotnet\" --targetargs \"test -c Release -f %s %s --no-build\" --format opencover --output \"./%s\""
                dllPath framework projectPath reportPath
            |> CreateProcess.fromRawWindowsCommandLine "coverlet"
            |> Proc.run
            |> ignore

            Trace.publish ImportData.BuildArtifact reportPath

            if isAppveyor then
                CreateProcess.fromRawWindowsCommandLine "codecov" (sprintf "-f \"%s\"" reportPath)
                |> Proc.run
                |> ignore
        )
)

Target.create "DeployGitHub" (fun _ -> 
    let gitHubToken = Environment.environVarOrNone("GITHUB_TOKEN")
    if(gitHubToken.IsNone) then
        Trace.traceError "GITHUB_TOKEN is not defined"
    else
        let (gitOwner, gitName) =
            AppVeyor.Environment.RepoName.Split('/')
            |> Array.pairwise
            |> Array.head


        GitHub.createClientWithToken gitHubToken.Value
        |> (fun clientAsync -> 
            async {
                let! client = clientAsync
                let releaseClient = client.Repository.Release
                let! release = releaseClient.Get(gitOwner, gitName, AppVeyor.Environment.RepoTagName) |> Async.AwaitTask
                if release <> null then
                    Trace.traceErrorfn "Release at %s already exists" AppVeyor.Environment.RepoTagName
                else
                    let isPrerelease = not(String.isNullOrWhiteSpace gitVersion.PreReleaseTag)
                    let releaseName = sprintf "%s - v%s" AppVeyor.Environment.ProjectName gitVersion.SemVer
                    let releaseBody = sprintf "## %s" releaseName

                    let newRelease = new Octokit.NewRelease(AppVeyor.Environment.RepoTagName);
                    newRelease.Name <- releaseName
                    newRelease.Body <- releaseBody
                    newRelease.Draft <- true
                    newRelease.Prerelease <- isPrerelease

                    let! release = releaseClient.Create(gitOwner, gitName, newRelease) |> Async.AwaitTask
                
                    let release : GitHub.Release = {
                        Client = client;
                        Owner = gitOwner;
                        RepoName = gitName;
                        Release = release
                    }
            
                    let files = !! "nuget/*.nupkg"

                    release
                    |> async.Return
                    |> GitHub.uploadFiles files
                    |> GitHub.publishDraft
                    |> Async.RunSynchronously
            }
        )
        |> Async.RunSynchronously


        GitHub.createClientWithToken gitHubToken.Value
        |> (fun clientAsync -> 
            async {
                let! client = clientAsync
                let releaseClient = client.Repository.Release
                let newRelease = new Octokit.NewRelease(AppVeyor.Environment.RepoTagName);
                newRelease.Name <- releaseName
                newRelease.Body <- releaseBody
                newRelease.Draft <- true
                newRelease.Prerelease <- isPrerelease

                let! release = releaseClient.Create(gitOwner, gitName, newRelease) |> Async.AwaitTask
                
                let release : GitHub.Release = {
                    Client = client;
                    Owner = gitOwner;
                    RepoName = gitName;
                    Release = release
                }

                return release
            }
        )
)

Target.create "DeployNuGet" (fun _ -> 
    ()
)

Target.create "DeployChocolatey" (fun _ -> 
    ()
)

Target.create "Default" (fun _ -> 
    ()
)

open Fake.Core.TargetOperators
"Clean" ==> "Build"

"Build" ==> "Package" ==> "Default"
"Build" ==> "Test" ==> "Default"
"Build" ==> "Coverage" ==> "Default"

//let shouldDeploy = isAppveyor && AppVeyor.Environment.RepoTag
let shouldDeploy = true

"Package" =?> ("DeployGitHub", (shouldDeploy)) ==> "Default"
"Package" =?> ("DeployNuGet", (shouldDeploy)) ==> "Default"
"Package" =?> ("DeployChocolatey", (shouldDeploy)) ==> "Default"

// start build
Target.runOrDefault "Default"
