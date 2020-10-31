Feature: SiteSearching
	In order to make sure the searching works fine
	I would like to test some common scenarios

@Title-Chrome
Scenario: [Chrome] Title Contains Searched Word
	Given the browser type is Chrome
	And the search word is automation
	And the search result link number to open is 1
	When the search result link is opened
	Then the title should contain searched world automation

@Title-Firefox
Scenario: [Firefox] Title Contains Searched Word
	Given the browser type is Firefox
	And the search word is automation
	And the search result link number to open is 1
	When the search result link is opened
	Then the title should contain searched world automation

@Domain-Chrome
Scenario: [Chrome] Searched Results Contain Domain
	Given the browser type is Chrome
	And the search word is automation
	And the search pages count is 5
	And the search domain is testautomationday
	When the search is done
	Then the searched results should contain domain testautomationday

@Domain-Firefox
Scenario: [Firefox] Searched Results Contain Domain
	Given the browser type is Firefox
	And the search word is automation
	And the search pages count is 5
	And the search domain is testautomationday
	When the search is done
	Then the searched results should contain domain testautomationday