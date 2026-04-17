# Agent Notes

## VS Code CLI

On this machine, `code` in PowerShell resolves to `Code.exe` directly (the Electron binary), which silently swallows CLI arguments like `--install-extension` and `--list-extensions`.

Use the proper CLI wrapper instead:

```powershell
$codeCli = "C:\Users\Ihor\AppData\Local\Programs\Microsoft VS Code\bin\code.cmd"
```

### Examples

```powershell
# List installed extensions
& $codeCli --list-extensions

# Install a VSIX
& $codeCli --install-extension "path\to\extension.vsix"

# Uninstall an extension
& $codeCli --uninstall-extension publisher.extension-id

# Check version
& $codeCli --version
```

## Packaging

```powershell
npx @vscode/vsce package --allow-missing-repository --allow-star-activation
```

This produces `eppz-code-modern-<version>.vsix` in the project root.

## Installing After Packaging

```powershell
& $codeCli --install-extension "c:\Development\VSCode.Extension.eppz_Code-modern\eppz-code-modern-1.0.0.vsix"
```

Reload VS Code after installing (`Ctrl+Shift+P` → `Developer: Reload Window`).
