rm -rf obj
rm -rf bin

dotnet build RF5.HisaCat.DialogueSkipper.csproj -f net6.0 -c Release

zip -j 'RF5.HisaCat.DialogueSkipper_v1.1.1.zip' './bin/Release/net6.0/RF5.HisaCat.DialogueSkipper.dll' './bin/Release/net6.0/RF5.HisaCat.DialogueSkipper.pdb'