% Факты
male(john).
female(mary).
parent(john, mary).

% Правило
father(X, Y) :- male(X), parent(X, Y).
