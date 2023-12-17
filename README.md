# Game Design Document

# Résumé : 

Le but de ce projet est de réaliser un pacman 3D en réalité virtuelle. Nous contrôlons un personnage qui se déplace en première personne (le casque), qui doit ramasser des pièces afin de terminer le niveau. Cependant le joueur est coursé par des  fantômes avec des comportements différents qui le tue dès qu’il le touche. Un niveau se déroulerait sur plusieurs étages représentant accentuant l’aspect 3D de cette version de pacman.

# Base :

Caméra : 
Première personne (le casque VR)

Control : 
Mouvements 8 directions (joystick gauche)
interaction avec des éléments comme des portes, téléporteurs ou autres objets (interaction main droite / main gauche)
une map de l’étage où l’on est (affichage dans la main gauche)
on peut regarder les points qu’ils nous manquent dans les différents étages (main gauche)
Pouvoir faire “pause” afin de retourner au menu (bouton select)

Character : 
Le personnage est invisible (juste caméra), ajouter un modèle de main ?

Agencement d’un niveau : 

# Niveau:
Un niveau se déroule dans un bâtiment à étages. Le personnage ne peut être que dans un étage à la fois. Les niveaux sont prévus d’être créés à la main.

## Etage:
Les étages sont sous la forme de mini-labyrinthe comme dans le jeu pacman original. Un étage contient des pièces et des téléporteurs afin de se déplacer entre les étages supérieurs ou inférieurs.

## Téléporteurs:
Les téléporteurs ont 2 actions possibles : aller à l’étage du dessous ou aller à l’étage du dessus. 
Des objets avec certaines interactions peuvent être dans les étages comme des portes à ouvrir ou des lampes à allumer.

## Maps:
La map dans la main gauche affiche l’étage actuel, ce qui oblige le joueur à se déplacer à un étage pour avoir la position des objets et fantômes. On voit aussi les téléporteurs sur la map.

Le résumé des pièces manquantes dans les étages sont affichés aussi dans la main gauche (en échange de la map de l’étage).

## Objets que l’on pourrait ajouter :
Portes
Interrupteurs lumière
objets quelconques à mettre dans ses mains
téléporteur piège à un étage aléatoire
lampe pour éloigner un fantôme
panier de basket

# IAs (fantômes) : 

Les fantômes ont plusieurs systèmes d’approches comme dans le pacman original. De base les fantômes restent dans leur étage et n’en changent pas.

## IA-1:
Fait des mouvements aléatoires.

## IA-2:
Suit le joueur avec le plus court chemin.

## IA-3:
Suit le joueur de loin et peut se déplacer entre les étages.

## IA-4:
Il fuit le joueur quand il s’approche trop.

## IA-5:
Suit le joueur avec le plus court chemin mais se stoppe dès qu’on le regarde.

## IA-6:
Elle se déplace que quand on se déplace.

## IA-7:
Ne se déplace qu’à partir d’un certain temps (5 min).


# Contraintes qui pourraient arriver :

L’un des plus gros problème avec le concept de ce jeu pourrait se trouver dans la “motion sickness” qui survient quand on se déplace en réalité virtuelle. Le concept de Pacman veut que l’on se déplace sans interruption. Ici on ne se déplace pas sans interruption mais il faut pouvoir se mouvoir rapidement pour esquiver les fantômes.

Un moyen pour résoudre le problème serait de déplacer un personnage à notre place et téléporter la caméra au joueur dès qu’on relâche le bouton. Cela ajouterait un modèle 3D pour le personnage et les animations qui vont avec.

Un autre moyen pour résoudre le problème de motion sickness serait de changer totalement le gameplay et que l’on ait une vue troisième personne au dessus de l’étage et guider notre personnage dans celui-ci.
