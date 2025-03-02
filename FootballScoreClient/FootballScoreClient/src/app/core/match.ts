export class Area {
    id!: number;
    name!: string;
    code!: string;
    flag!: string;
}

export class Competition {
    id!: number;
    name!: string;
    code!: string;
    type!: string;
    emblem!: string;
}

export class Season {
    id!: number;
    startDate!: string;
    endDate!: string;
    currentMatchday!: number;
    winner: string | null = "";
}

export class Team {
    id!: number;
    name: string = "";
    shortName: string = "";
    tla: string = "";
    crest?: string = "";
}

export class Score {
    winner?: string | null;
    duration?: string = "";
    fullTime!: { home: number | null; away: number | null };
    halfTime!: { home: number | null; away: number | null };
}

export class Referee {
    id!: number;
    name!: string;
    type!: string;
    nationality!: string;
}

export class Match {
    area!: Area;
    competition!: Competition;
    season!: Season;
    id!: number;
    utcDate!: string;
    status!: string;
    matchday!: number;
    stage!: string;
    group!: string | null;
    lastUpdated!: string;
    homeTeam!: Team;
    awayTeam!: Team;
    score!: Score;
    odds!: { msg: string };
    referees: Referee[] = [];
}

export class ResultSet {
    count!: number;
    competitions!: string;
    first!: string;
    last!: string;
    played!: number;
}

export class Filters {
    dateFrom!: string;
    dateTo!: string;
    permission!: string;
}

export class MatchData {
    filters!: Filters;
    resultSet!: ResultSet;
    matches: Match[] = [];
}
