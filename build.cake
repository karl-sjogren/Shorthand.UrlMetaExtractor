var target = Argument("target", "build");
var output = Argument("output", "./artifacts");
var versionSuffix = Argument<string>("versionSuffix", null);

Task("clean")
    .Does(() => {
        CleanDirectories("./src/**/bin/");
        CleanDirectories("./src/**/obj/");
        CleanDirectories("./test/**/bin/");
        CleanDirectories("./test/**/obj/");
        CleanDirectory("./artifacts");
    });   

Task("build")
    .IsDependentOn("clean")
    .Does(() => {
        DotNetCoreRestore("./src/Shorthand.UrlMetaExtractor/Shorthand.UrlMetaExtractor.csproj");
        
        var buildSettings = new DotNetCoreBuildSettings {
            Configuration = "Release",
            VersionSuffix = versionSuffix
        };

        DotNetCoreBuild("./src/Shorthand.UrlMetaExtractor/Shorthand.UrlMetaExtractor.csproj", buildSettings);
    });

Task("pack")
    .Does(() => {
        var packSettings = new DotNetCorePackSettings{
            Configuration = "Release",
            OutputDirectory = output,
            VersionSuffix = versionSuffix
        };

        DotNetCorePack("./src/Shorthand.UrlMetaExtractor/Shorthand.UrlMetaExtractor.csproj", packSettings);
    });

Task("test")
    .Does(() => {
        var settings = new DotNetCoreTestSettings { };

        DotNetCoreRestore("./tests/Shorthand.UrlMetaExtractor.Tests/Shorthand.UrlMetaExtractor.Tests.csproj");                
        DotNetCoreTest("./tests/Shorthand.UrlMetaExtractor.Tests/Shorthand.UrlMetaExtractor.Tests.csproj", settings);
    });

RunTarget(target);