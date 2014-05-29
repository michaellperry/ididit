convert AppTile.png -resize "30x30>" ..\IDidIt\IDidIt.Windows\Assets\SmallLogo.scale-100.png
convert AppTile.png -resize "50x50>" ..\IDidIt\IDidIt.Windows\Assets\StoreLogo.scale-100.png
convert AppTile.png -resize "150x150>" ..\IDidIt\IDidIt.Windows\Assets\Logo.scale-100.png
convert AppTile.png -resize "620x300>" -size 620x300 xc:transparent +swap -gravity center -composite ..\IDidIt\IDidIt.Windows\Assets\SplashScreen.scale-100.png
convert AppTile.png -resize "170x170>" ..\IDidIt\IDidIt.WindowsPhone\Assets\Square71x71Logo.scale-240.png
convert AppTile.png -resize "360x360>" ..\IDidIt\IDidIt.WindowsPhone\Assets\Logo.scale-240.png
convert AppTile.png -resize "106x106>" ..\IDidIt\IDidIt.WindowsPhone\Assets\SmallLogo.scale-240.png
convert AppTile.png -resize "120x120>" ..\IDidIt\IDidIt.WindowsPhone\Assets\StoreLogo.scale-240.png
convert AppTile.png -resize "744x360>" -size 744x360 xc:transparent +swap -gravity center -composite ..\IDidIt\IDidIt.WindowsPhone\Assets\WideLogo.scale-240.png
convert AppTile.png -resize "744x360>" -size 1152x1920 xc:transparent +swap -gravity center -composite ..\IDidIt\IDidIt.WindowsPhone\Assets\SplashScreen.scale-240.png
