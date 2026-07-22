namespace VoteLibrary;

public class Vote
{
    private List<(string, int)> candidates = new();
    private int round = 1;
    private bool end = false;

    private string? WinnerFirstRound()
    {
        foreach ((string candidate, int res) in candidates)
        {
            if (res >= 50)
            {
                return candidate;
            }
        }
        return null;
    }

    private string? WinnerSecondRound()
    {
        if (candidates.Count == 0)
        {
            return null;
        } else if (candidates.Count > 1 && candidates.All(x => x.Item2 == candidates[0].Item2))
        {
            return null;
        }
        return candidates.MaxBy(x => x.Item2).Item1;
    }

    public string? Winner()
    {
        if (!end)
        {
            throw new Exception("Vote isn't complete !");
        }
        return this.round == 1 ? WinnerFirstRound() : WinnerSecondRound();
    }

    public void RegisterCandidate(string candidate, int votesPercentage = 0)
    {
        if (end)
        {
            throw new Exception("Vote ended, cannot register new candidates !");
        } else if (round == 2)
        {
            throw new Exception("2nd round, cannot add new candidates !");
        }
        candidates.Add((candidate, votesPercentage));
    }

    public void SetSecondRoundVotes(string candidate, int votes)
    {
        if (round == 1)
        {
            throw new Exception("Only set vote on the 2nd round !");
        }

        for (int i = 0; i < candidates.Count; i++)
        {
            if (candidate == candidates[i].Item1)
            {
                candidates[i] = (candidate, votes);
                return;
            }
        }
        throw new Exception("Cannot find candidate !");
    }

    private void PurgeCandidatesForSecondRound()
    {
        candidates.OrderBy(x => x.Item1).Reverse();
        candidates = candidates.GetRange(0, 2);
    }

    private void NextRound()
    {
        if (round == 2)
        {
            throw new Exception("Already at the 2nd turn !");
        }
        round = 2;
        PurgeCandidatesForSecondRound();
    }

    public void EndRound()
    {
        if (round == 1 && WinnerFirstRound() is null)
        {
            NextRound();
        } else
        {
            end = true;
        }
    }

    public bool IsEnd()
    {
        return end;
    }
}
