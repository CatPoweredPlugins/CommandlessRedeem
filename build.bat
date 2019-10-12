del .\Ryzhehvost.CommandlessRedeem\*.zip
dotnet publish -c "Release" -f "net48" -o "out/generic-netf"
rename .\Ryzhehvost.CommandlessRedeem\CommandlessRedeem.zip CommandlessRedeem-netf.zip 
dotnet publish -c "Release" -f "netcoreapp3.0" -o "out/generic" "/p:LinkDuringPublish=false"