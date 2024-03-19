# Deploying a new version of Cooke

## Update AppConfig.CreateDefaultConfigFile()

The `CookeBuild` value should be in `YYMMMDD.HHMM` for development versions, and `YYMMMDD` for releases.

For example, prior to releasing a new version of Cooke, change this:

`CookeBuild = "240319.0950",`

to this:

`CookeBuild = "240319",`