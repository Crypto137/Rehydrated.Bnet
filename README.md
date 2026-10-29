# Rehydrated.Bnet

Rehydrated.Bnet is a minimal Diablo 3 online service emulator for connecting to a local game server. This is intended to be used with the PC proxy mode of [Rehydrated](https://github.com/Crypto137/Rehydrated).

The only supported client version is 1.0.8.16603.

## Setup

1. Build the project by installing the .NET 8 SDK and running the `dotnet build` command on `Rehydrated.Bnet.sln`.

2. Open `battle.net.dll` under `Bnet` in your client directory with a hex editor and apply the following patches:
   
   1. Disable authentication: `74 24` -> `90 90` at `0x166371`.
   
   2. Disable protocol encryption: `55 8B EC` -> `B0 01 C3` at `0x1CC520`.

3. Open `Diablo III.exe` with a hex editor and apply the following patch to disable the Agent initialization: `55` -> `C3` at `0x7486A0`. This will resolve some of the long loading and stuttering you may experience.

4. Run `Rehydrated.Bnet.exe`.

5. Run `Diablo III.exe` with the following arguments: `-launch OnlineService.Host=localhost:1119`.

6. Log in with any email and password.
