import { Area, Season } from "./match";

export class LeagueResponse {
    count!: number;
    competitions!: League[];
}

export class League {
    id!: number;
    area!: Area;
    name!: string;
    code!: string;
    type!: string;
    emblem!: string;
    plan!: string;
    currentSeason!: Season;
    numberOfAvailableSeasons!: number;
    lastUpdated!: Date;
}