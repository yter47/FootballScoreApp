import { League } from "./league";
import { Area, Filters, Season, Team } from "./match";

export class Standings {
    stage!: string;
    type!: string;
    group!: string;
    table!: TeamStanding[];
}

export class StandingsResponse {
    filters!: Filters;
    area!: Area;
    competition!: League;
    season!: Season;
    standings!: Standings[]
}

export class TeamStanding{
    position!: number;
    team!: Team;
    playedGames!: number;
    form?: string = "";
    won!: number;
    draw!: number;
    lost!: number;
    points!: number;
    goalsFor!: number;
    goalsAgainst!: number;
    goalDifference!: number;
}

