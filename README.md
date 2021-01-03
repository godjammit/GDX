# GDX
[![Licence](https://img.shields.io/github/license/midlevel/mlapi.svg?color=informational)](https://github.com/MidLevel/MLAPI/blob/master/LICENCE)

Game Development Extensions, a battle-tested library of game-focused high performance C# code.

## Feature Highlights
- Optimized & non-allocating collections (_GDX.Collections_)
  > It's important to note that many of the structures backing data, indices, counts, etc. are publicly accessible. 
  > This is meant for _advanced usage_, **change at runtime at your own risk**.

## Usage
Add `com.dotbunny.gdx` as a dependency to the project `Packages/manifest.json` file:

```
{
  "dependencies": {
    "com.dotbunny.gdx": "https://github.com/dotBunny/GDX.git",
  }
}
```
> It is possible for the repository to be simply cloned into a sub-folder in your projects Asset folder.

## Requirements
The package is designed to be compatible with an _empty project_ created in [Unity](http://unity3d.com). 
It uses a preprocessor system, where the assembly definition will define features based on the packages found in the project.
> It is important to note that the GDX_* defines are only valid inside of the GDX assemblies.
### Supported Packages
Package | Minimum Version
:--- | ---
com.unity.addressables | `1.16.0`
com.unity.burst | `1.4.0`
com.unity.jobs | `0.6.9`
com.unity.mathematics | `1.2.1`

## Contributing
GDX is an open-source project and we encourage and welcome contributions.
### Design Guidelines
There is a general effort to follow the [Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/) 
set forth by the .NET team. While we **do not** always precisely adhere to them, they serve as a guiding principle.
- Embedded in the project is an [EditorConfig](https://editorconfig.org/), which should standardize much of the formatting. 
  - It is based on the .NET Roslyn repositories `.editorconfig`. 
  - Warns of non-explicit type definitions everywhere (we're not going to use var to promote better readability). 
- [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions) is also a good point of reference.
- Typically sections of classes are ordered alphabetically.
- Preference to expose backing data, indices, etc.

## License
GDX is licensed under the [MIT License](https://choosealicense.com/licenses/mit/).
A copy of this license can be found at the root of the project in the `LICENSE` file.