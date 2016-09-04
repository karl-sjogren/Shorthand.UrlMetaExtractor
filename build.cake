#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "default");
var configuration = Argument("configuration", "Release");
var output = Argument("output", "./artifacts");

Information("Target: " + target);
Information("Configuration: " + configuration + ", tests always run under Debug");

Task("clean")
    .Does(() => {
        CleanDirectories("./**/bin/" + configuration);
        CleanDirectories("./**/obj/" + configuration);
        CleanDirectory("./artifacts");
    });    

Task("restore")
    .IsDependentOn("clean")
    .Does(() => {
        DotNetCoreRestore("src/Shorthand.UrlMetaExtractor/project.json");
    });

Task("build")
    .IsDependentOn("restore")
    .Does(() => {
        var buildSettings = new DotNetCoreBuildSettings {
            Configuration = configuration
        };

        DotNetCoreBuild("src/Shorthand.UrlMetaExtractor/project.json", buildSettings);
    });

Task("test")
    .IsDependentOn("restore")
    .Does(() => {
        var settings = new DotNetCoreTestSettings {
			WorkingDirectory = "./tests/Shorthand.UrlMetaExtractorTests/"
		};
        DotNetCoreRestore("./tests/Shorthand.UrlMetaExtractorTests/project.json");                
        DotNetCoreTest("./project.json", settings);
    });

Task("pack")    
    .IsDependentOn("build")
    .Does(() => {
        var packSettings = new DotNetCorePackSettings {
            Configuration = configuration,
            OutputDirectory = output
        };

        DotNetCorePack("src/Shorthand.UrlMetaExtractor/project.json", packSettings);
    });

Task("default")
    .IsDependentOn("test");

RunTarget(target);
