del .\Ryzhehvost.CommandlessRedeem\*.zip
dotnet publish -c "Release" -f "net472" -o "out/generic-netf"
rename .\Ryzhehvost.CommandlessRedeem\CommandlessRedeem.zip CommandlessRedeem-netf.zip 
dotnet publish -c "Release" -f "netcoreapp2.2" -o "out/generic" "/p:LinkDuringPublish=false"