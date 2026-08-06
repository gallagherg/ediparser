# Building the documentation

Install DocFX as a local or global .NET tool, then run from the `docs` folder:

```powershell
docfx metadata docfx.json
docfx build docfx.json
```

To preview locally:

```powershell
docfx serve _site
```

The generated website is written to `docs/_site`.

## Expected repository location

This folder assumes the Core project is located at:

```text
src/EDIParser.Core/EDIParser.Core.csproj
```

If your repository uses a different path, update the project path in `docfx.json`.
