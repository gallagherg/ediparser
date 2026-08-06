# Sample Projects

The repository includes migrated .NET 8 samples that demonstrate common parser workflows.

Typical sample categories include:

- reading X12 transactions
- generating X12 transactions
- translating transaction types
- parsing HL7 messages
- reading EDIFACT messages
- database-backed samples using `System.Data.OleDb`
- the WinForms Viewer

## Build the samples

From the repository root:

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\clean-restore-build.ps1
```

## OleDb samples

Database samples require the `System.Data.OleDb` package and an installed compatible OLE DB provider for the database format being opened.

## Excluded legacy samples

Obsolete WCF samples and registration/licensing demonstrations are not part of the supported .NET 8 build.
