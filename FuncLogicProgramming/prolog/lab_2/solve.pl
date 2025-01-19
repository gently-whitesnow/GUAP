solve(
    YanoshName, YanoshRank, YanoshBranch,
    FerencName, FerencRank, FerencBranch,
    BelaName, BelaRank, BelaBranch,
    LaioshName, LaioshRank, LaioshBranch,
    AndrashName, AndrashRank, AndrashBranch
) :-
    % Определяем имена
    YanoshName = yanosh,
    FerencName = ferenc,
    BelaName = bela,
    LaioshName = laiosh,
    AndrashName = andrash,

    % Возможные звания и ветви войск
    Ranks = [captain, major, major, major, lieutenant_colonel],
    Branches = [infantry, artillery, pilot, communications, sapper],

    % инициализация
    permutation(Ranks, [YanoshRank, FerencRank, BelaRank, LaioshRank, AndrashRank]),
    permutation(Branches, [YanoshBranch, FerencBranch, BelaBranch, LaioshBranch, AndrashBranch]),

    % 1. У Яноша такое же звание, как у его друга сапера
    YanoshBranch \= sapper,
    member(SapperRank, Ranks),
    YanoshRank = SapperRank,

    % 2. Офицер-связист и Ференц — большие друзья
    FerencBranch \= communications,

    % 3. Летчик недавно был в гостях у Ференца
    BelaBranch \= pilot,
    LaioshBranch \= pilot,
    FerencBranch \= pilot,

    % 4. Незадолго до званого вечера у артиллериста и сапера почти одновременно
    LaioshBranch \= artillery,
    LaioshBranch \= sapper,
    LaioshBranch \= communications,

    % 5. Ференц чуть было не стал летчиком, но потом по совету своего друга сапера
    FerencBranch \= sapper,

    % 6. Янош по званию старше Лайоша, а Бела старше Ференца
    higher_rank(YanoshRank, LaioshRank),
    higher_rank(BelaRank, FerencRank),

    format("~w ~w ~w~n", [YanoshName, YanoshRank, YanoshBranch]),
    format("~w ~w ~w~n", [FerencName, FerencRank, FerencBranch]),
    format("~w ~w ~w~n", [BelaName, BelaRank, BelaBranch]),
    format("~w ~w ~w~n", [LaioshName, LaioshRank, LaioshBranch]),
    format("~w ~w ~w~n", [AndrashName, AndrashRank, AndrashBranch]).
    
    
% Правило для сравнения званий
higher_rank(lieutenant_colonel, major).
higher_rank(major, captain).
