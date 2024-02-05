# World Cup Score Board

## Entities

### Match
1.    Description
    Encaplulates a football match information. Can start a match, finish it and change scores.


2.   Assumptions 

    We don't check any match related information. 
    The only assumption is that the match should be Started for scores to be updated.
    We don't check if start date is a date in the past or in the future. It is done to avoid match time/etxta time calculations.
    

### Scoreboard

1.    Description
    Scoreboard is responsible for the scoreboard logic. 
    It has a list of matches and encapsulates a logic for starting, finishing and updating scores.


2.   Assumptions 

    We don't check any match time related information. The start time is used only for sorting the scoreboard items.
    Also, there is no thread safety check or any synchronization logic. It is done for simplicity.
