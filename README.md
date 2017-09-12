# A LESS compiler for ASP.NET Core

[![Build status](https://ci.appveyor.com/api/projects/status/dtkck6ymi5rjykln?svg=true)](https://ci.appveyor.com/project/madskristensen/weboptimizer-less)
[![NuGet](https://img.shields.io/nuget/v/LigerShark.WebOptimizer.LESS.svg)](https://nuget.org/packages/LigerShark.WebOptimizer.LESS/)

This package compiles TypeScript, ES6 and JSX files into ES5 by hooking into the [LigerShark.WebOptimizer](https://github.com/ligershark/WebOptimizer) pipeline.

## Usage

You can reference any LESS file directly in the browser and a compiled CSS document will be served. To set that up, do this:

```c#
services.AddWebOptimizer(pipeline =>
{
    pipeline.CompileLessFiles();
});
```

Or if you just want to parse specific LESS files, do this:

```c#
services.AddWebOptimizer(pipeline =>
{
    pipeline.CompileLessFiles("/path/a.less", "/path/b.less");
});
```