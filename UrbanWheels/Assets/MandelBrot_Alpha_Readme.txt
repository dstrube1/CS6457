Author Steven Deam (sdeam13@gatech.edu)

Game Functionality:
    Drive the vehicles:
        W to accelerate
        A/D to turn left and right
        S to brake
        Left Click to shift gear up
        Right Click to shift gear down
        SpaceBar to perform 'Action' -- turn or perform next quest action (pick up or drop off)

Unity Development:
    Created Game Manager Shell
        Central repository of Game Scope information
    Created PlayerCharater Prefab
        Allows for switchable character vehicles over time
    Created City Shell
        Location for the generated city blocks to instantiate
    Created CityBlock Prefab
        City Blocks are the building blocks of the city generation functionality
        Defined Turning zones, quest zones, and exit locations for code references
Script Development
    PlayerCharacter
        Key Variables
            Max Vehicle Speed
            Number of Gears -- these two combines
            isTurning -- an indicator if the vehicle is currently processing a turn
        Major Functionality:
            Uses Unity's Input system to provide controls to the vehicle
            ExecuteMove
                Determines acceleration and lane shifting then tranlates the vehicle in the appropriate direction(s)
            CheckTurnCollision
                As an endless runner, side to side movement is typically for obstacle avoidance, turns need to be triggered by the action button
                This function checks determines the quality of turn based on timing, and the end location of the turn
            ExecuteTurn
                If isTurning is flagged, this executed a turn to a specific target
            SetNearestExit
                After a turn is initiated, the next chunk of the city will be built, this sets the location for the next city chunk
    CityGenerator
        Key Variables
            GameObject[] various items
                these arrays store different prefabs for the variety of turns available
            CurrentStraight
                Each city chunk will be a series of straight road pieces, this stores the current list of prefabs the player is driving on
        Major Functionality
            GenerateStraightSection
                Groups items that are marked as 'Straights' and then selects one at random and places it in places
                it then determines the orientation and location of the city block and sets the exit location values on instantiated pieces
            PlaceAndOrientChunk
                Instantiates the selected city block and places it at the location determined by the Generate Functionality
                Sets the city block within the City container in the unity inspector. 

            
            
            
