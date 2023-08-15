# Gitignorerer
A command line tool designed to automate the process of creating gitignore files.

## What is this?
If you hate writing gitignores as much as I do, you typically use prewritten gitignore files, either through an IDE plugin or just old-fashioned copy/paste. This tool automates that process in the command line, using Github's [gitignore API](https://docs.github.com/en/rest/gitignore?apiVersion=2022-11-28).

## How do I use it?
The app is distributed as a dotnet tool, so make sure you have the dotnet CLI installed and use this command to install globally, allowing for use like any other CLI app:

```sh
dotnet tool install --global gitignorerer
```

Once installed, the tool will create or overwrite a .gitignore file in the current directory using list of "ignore files", typically languages as below:

```sh
gitignorerer VisualStudio C++
```

This command will write the ignorefiles for VisualStudio and C++ to the local file.
