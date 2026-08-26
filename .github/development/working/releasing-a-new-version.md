# Releasing a new version of Cooke

## Update AppConfig.BuildContent()

The `AppVer` value should be in `X.Y.Z.YYMMMDD` for development versions, and `X.Y.Z` for releases.

For example, prior to releasing a new version of Cooke, change this:

`AppVer = "0.1.2.40319",`

to this:

`AppVer = "0.1.2",`