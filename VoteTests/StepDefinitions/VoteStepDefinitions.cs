using VoteLibrary;

namespace VoteBase.StepDefinitions
{
    [Binding]
    public sealed class VoteStepDefinitions
    {
        // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef

        private readonly Vote _target = new Vote();

        #region Given

        [Given("candidates are")]
        public void GivenCandidatesAre(Table table)
        {
            foreach (DataTableRow row in table.Rows)
            {
                this._target.RegisterCandidate(row[0], int.Parse(row[1]));
            }
        }

        #endregion

        #region when

        [When("vote ends")]
        public void WhenVoteEnds()
        {
            this._target.EndRound();
        }

        [Given("candidates of the 2nd round are")]
        public void When2ndRound(Table table)
        {
            this._target.EndRound();
            Assert.IsFalse(this._target.IsEnd());
            foreach (DataTableRow row in table.Rows)
            {
                this._target.SetSecondRoundVotes(row[0], int.Parse(row[1]));
            }
        }

        #endregion

        #region Then

        [Then("the winner should be (.*)")]
        public void ThenTheWinnerShouldBe(string winner)
        {
            Assert.AreEqual(winner, this._target.Winner());
        }

        #endregion
    }
}
