# Game Design Document

# Membres
- Von Euw Félix
- Emilien Noyer
- Decabrat Tanguy
- Miqueu Eloya
- Cerello Pauline
- Pelet Antoine

# Répartition des taches :
- Management : Félix (100%)
- Création de niveaux (Design) : Emilien (25%), Antoine (40%), Pauline (35%)
- Génération des niveaux (Dev) : Antoine + Pauline (Fail 100%), Tanguy (Réussite 70%), Félix (Réussite 30%)
- IA : Tanguy (90%), Felix (8%), Emilien (2%)
- Téléporteurs (inter-étage) : Emilien (80%), Félix (20%)
- Téléporteurs (entre-étages) : Emilien (40%), Félix (55%), Tanguy (5%)
- Pièces : Emilien (55%), Félix (45%)
- Tracking des pièces (texte) : Emilien (60%), Félix (40%)
- Modèles 3D : Eloya (50%), Antoine + Pauline (50%)
- Textures : Félix (60%), Antoine + Pauline (40%)
- Player (VR + Script) : Félix (100%)
- Menu : Félix (70%), Emilien (30%)
- Décorations des scènes et niveaux : Félix (50%), Emilien (50%)
- Optimisations (occlusion culling, light baking, etc) : Félix (65%), Emilien (35%)
- Minimap : Tanguy (70%), Félix (30%)
- Placement des objets importants : Tanguy (50%), Félix (50%)
- Sons + Musique (Faits depuis https://sfxr.me et https://www.epidemicsound.com) : Emilien (95%), Pauline (5%)
- Tests VR : Tanguy (40%), Félix (40%), Antoine (15%), Emilien (5%)
- Fix merge conflicts : Tanguy (50%), Félix (25%), Emilien (25%)
- Twitch : Félix (100%)

# Outils utilisés :
- Unity 3D (moteur de jeu)
- Blender (modélisation 3D)
- Krita (dessins 2D)
- Pixabay (site de musique)
- Google Sheet (design niveaux)

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
