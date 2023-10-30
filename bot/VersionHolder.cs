using System;

namespace YordleYelper.bot; 

public static class VersionHolder {
    public static string Version { get; private set; } = string.Empty;
    
    public static void Init(string version) {
        if (Version != string.Empty) {
            throw new Exception("Already init!");
        }

        Version = version;
    }
}