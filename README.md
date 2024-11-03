# OSK.Extensions.Object.DeepEquals
A fast, flexible, and easy mechanism for deep comparison between objects. This is provided as an extension to the base object.

# Usage

Using the extension, the standard default comparers can be used for most basic scenarios and can be accessed easily by using:
`a.DeepEquals(b)`

For more advanced scenarios, deem comparison options can be overrided by using:

`a.DeepEquals(b, options)`

or by setting custom configurations using the `DeepEqualsConfiguration` helper. Using the global configuration will allow for adding, replacing, or otherwise altering the base Deep Equality comparers and other comparison options.

# Issues
Please provide issues using the repository's issue tracker tab.

# Contributions
Contributions by the community, for additional comparers, bug fixes, etc. is encouraged and welcome. Please create a pull request for your updates for review.