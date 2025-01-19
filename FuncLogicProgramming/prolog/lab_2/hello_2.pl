% Факты
male(john).
female(mary).
female(susan).
parent(john, mary).
parent(john, susan).
parent(mary, tom).

% Правила
father(X, Y) :- male(X), parent(X, Y).
mother(X, Y) :- female(X), parent(X, Y).