Feature: Vote

Simple Vote system

Scenario: 1 round, 51% vs 49%
	Given candidates are
	| Candidates | Votes |
	| Ronald Plump | 49 |
	| Joe Widen | 51 |
	When vote ends
	Then the winner should be Joe Widen

Scenario: 1 round, 50+% with many candidates
	Given candidates are
	| Candidates | Vote |
	| Emmanuel Valse | 0 |
	| Annie Dingo | 1 |
	| Manuel Macaron | 2 |
	| Citroën AMI | 97 |
	When vote ends
	Then the winner should be Citroën AMI

Scenario: 1 round, tie with 2nd and 3rd candidates
	Given candidates are
	| Candidates | Vote |
	| IBM | 60 |
	| Google | 40 |
	| Meta | 40 |
	| Microsoft | 0 |
	When vote ends
	Then the winner should be IBM

Scenario: 2 rounds
	Given candidates are
		| Candidates | Votes |
		| C++ | 40 |
		| C | 35 |
		| Rust | 20 |
		| Python | 5 |
		| Java | 0 |
	And candidates of the 2nd round are
		| Candidate | Votes |
		| C++ | 45 |
		| C | 55 |
	When vote ends
	Then the winner should be C

Scenario: 2 rounds with tie
	Given candidates are
		| Candidates | Votes |
		| Mt Sain Michel --> Bretagne | 49 |
		| Mt Sain Michel --> Normandie | 49 |
		| Mt Sain Michel --> Corse | 2 |
	And candidates of the 2nd round are
		| Candidates | Votes |
		| Mt Sain Michel --> Bretagne | 50 |
		| Mt Sain Michel --> Normandie | 50 |
	When vote ends
	Then there is a tie

Scenario: End vote before asking for the winner
	Given candidates are
		| Candidates | Vote |
		| a | 0 |
		| b | 1 |
	Then the vote hasn't finished

Scenario: Check results
	Given candidates are
		| Candidates | Vote |
		| Pain au chocolat | 49 |
		| Chocolatine | 1 |
	When vote ends
	Then results should be
		| Results |
		| Pain au chocolat has 49 votes (98%) |
		| Chocolatine has 1 vote (2%) |
