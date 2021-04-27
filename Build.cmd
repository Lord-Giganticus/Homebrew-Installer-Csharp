pushd %CD%
cd Wii-or-VWii-Homebrew-Installer\Wii.or.VWii.Homebrew.Installer.CLI
dotnet publish -p:PublishProfile=Release
popd
copy Wii-or-VWii-Homebrew-Installer\Wii.or.VWii.Homebrew.Installer.CLI\bin\Release\net5.0\publish\win-x86\Wii.or.VWii.Homebrew.Installer.CLI.exe .
pushd %CD%
cd Wii-U-Homebrew-Installer\Wii.U.Homebrew.Installer.CLI
dotnet publish -p:PublishProfile=Release
popd
copy Wii-U-Homebrew-Installer\Wii.U.Homebrew.Installer.CLI\bin\Release\net5.0\publish\win-x86\Wii.U.Homebrew.Installer.CLI.exe .
7z a Release.7z Wii.or.VWii.Homebrew.Installer.CLI.exe && 7z a Release.7z Wii.U.Homebrew.Installer.CLI.exe
del /f Wii.or.VWii.Homebrew.Installer.CLI.exe && del /f Wii.U.Homebrew.Installer.CLI.exe
echo Complete!