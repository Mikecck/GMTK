using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : Singleton<BadgeManager>
{
    [System.Serializable]
    public class ThemeBadges
    {
        public string themeName;
        public List<bool> badges;
    }

    public List<ThemeBadges> allThemeBadges = new List<ThemeBadges>();

    // Call this method when a level is successfully completed
    public void AwardBadge(int themeIndex, int levelIndex)
    {
        if (themeIndex < allThemeBadges.Count && levelIndex < allThemeBadges[themeIndex].badges.Count)
        {
            allThemeBadges[themeIndex].badges[levelIndex] = true;
            Debug.Log($"Badge awarded for theme {themeIndex}, level {levelIndex}");
            CheckAllBadgesCollected(themeIndex);
        }
    }

    // Check if all badges in a theme are collected
    private void CheckAllBadgesCollected(int themeIndex)
    {
        foreach (bool badge in allThemeBadges[themeIndex].badges)
        {
            if (!badge) return;  // If any badge is not collected, return immediately
        }
        Debug.Log($"All badges collected for theme {themeIndex}");
        // Optional: trigger something special here, like unlocking the next theme
    }

    public bool AreAllBadgesCollected(int themeIndex)
    {
        foreach (bool badge in allThemeBadges[themeIndex].badges)
        {
            if (!badge) return false;
        }
        return true;
    }
}

