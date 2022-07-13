# linux
export DOTNET_ROOT="/usr/share/dotnet/shared"

# Mono (incorrect for LNX)
export PATH="/Library/Frameworks/Mono.framework/Versions/Current/Commands/mono:$PATH"
#correct
export PATH="$HOME/.dotnet/tools:$PATH"

# For dotnet 6 SDK:
export PATH="/usr/share/dotnet:/usr/share/dotnet/sdk:$PATH"

# Godot (linux)
# Path go Godot executable, it might look like this:
export GODOT="/usr/bin/godot-mono"