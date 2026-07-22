namespace VoteLibrary;

public class Vote
{
    private List<(string, int)> candidates = new();
    private int round = 1;
    private bool end = false;
    private int totalVotes => candidates.Sum(x => x.Item2);

    // 2nd candidate = 3rd candidate
    // 1st candidate < 50%
    // 1st candidate wins
    private string? WinnerFirstRoundByTieWithSecondAndThirdCandidate()
    {
        if (candidates.Count < 3)
        {
            return null;
        }
        List<(string, int)> orderedCandidates = candidates.OrderBy(x => x.Item2).Reverse().ToList();
        if (orderedCandidates[1].Item2 == orderedCandidates[2].Item2)
        {
            return orderedCandidates[0].Item1;
        }
        return null;
    }

    private string? WinnerFirstRound()
    {
        foreach ((string candidate, int votes) in candidates)
        {
            if ((decimal)votes / totalVotes > 0.5m)
            {
                return candidate;
            }
        }
        string? winner = WinnerFirstRoundByTieWithSecondAndThirdCandidate();
        if (winner is not null)
        {
            return winner;
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

    private string CandidateToString(string candidate, int votes)
    {
        string result = $"{candidate} has {votes} vote";
        if (votes >= 2)
        {
            result += 's';
        }

        float percentage = (float)votes * 100 / totalVotes;

        result += $" ({percentage.ToString("0.##")}%)";

        return result;
    }

    public List<string> SortedResults()
    {
        if (!end)
        {
            throw new Exception("Vote hasn't finished !");
        }
        return candidates.OrderBy(x => x.Item1).Reverse().Select(x => CandidateToString(x.Item1, x.Item2)).ToList();
    }
}
