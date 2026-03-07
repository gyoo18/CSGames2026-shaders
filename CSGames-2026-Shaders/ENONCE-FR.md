# Le super Shader challenge party

Heyo ! Je suis Noto 🤖 et j'aime le rétro ! Mais mon problème, c'est que plus personne ne fait du rétro aujourd'hui… les temps ont changé…
Mais tu peux m'aider à rendre le rétro génial à nouveau grâce aux shaders. J'ai quelques idées en tête, peut-être que tu sauras comment les réaliser ?

> Pour ce défi, tu devras écrire du code GLSL (shader) pour créer / transformer / générer / concevoir / reproduire des images.
> Tu utiliseras [DSD](https://github.com/Akahara/ShadersWorkspace/tree/master/DynamicShaderDisplay) ainsi que l'IDE de ton choix – VSCode avec l'extension HLSL fonctionne bien.
> DSD est un programme simple qui exécute des shaders et te permet d'éditer les uniforms (variables du shader) à l'exécution. Il utilise les commentaires après la déclaration des `uniform`, évite de les modifier.
>
> - Noto est un robot mais il est un peu égoïste, n'utilise aucune IA ! Pas de ChatGPT ni de Copilot. Tu peux cependant librement naviguer sur Internet.
> - Des commandes te seront fournies pour exécuter DSD : exécute `dsd run ...` avec `extern\jdk-27-windows\bin\java.exe -jar extern\dsd.jar -- run ...` ou crée-toi un alias / raccourci. Tu peux toujours trouver plus d'infos sur les commandes avec `dsd ? <command>` ou `dsd --help <command>`. **Fais attention aux lignes de commande : parfois tu exécuteras des fichiers .json au lieu de .fs.**

## Guide rapide pour écrire des shaders

> *Passe cette partie si tu sais déjà ce qu'est un shader*

Les shaders sont des programmes classiques, sauf qu'ils s'exécutent sur le GPU. On va rester simple en travaillant surtout avec des *fragment shaders* : l'idée est que la fonction `main` est exécutée une fois par pixel, avec une couleur en sortie par pixel.

Tu as accès aux uniforms (variables globales) ainsi qu'aux built-ins GLSL. Par exemple, si tu voulais rendre la moitié gauche de l'écran verte, tu écrirais :

```glsl
void main() {
    color = gl_FragCoord.x < u_resolution.x/2 ? vec4(0,0,1,1) : vec4(0,0,0,1);
}
```

Où `gl_FragCoord` est la position du pixel et `u_resolution` la taille de l'écran.

Les défis s'appuieront pas mal sur les maths, plutôt dans le sens "vraiment comprendre les maths simples" que dans le sens "[compute the distance to an isosurface that is defined through an implicit scalar field f(x)](https://iquilezles.org/articles/distance/)".

Tu peux trouver des fonctions utiles ici :
> https://www.desmos.com/calculator/le4fddxtws

La plupart des templates de code un peu documentés (**lisez les commentaires!**), et résoudre certains défis rendra les autres plus faciles. N'hésite pas à sauter des questions pour y revenir plus tard. Bonne chance !

## Les souvenirs de Noto (45)

🤖 J'ai quelques images sur ma clé USB de 4096 Ko, mais je ne me souviens plus comment je les ai faites. Peux-tu écrire du code pour les reproduire ?

> Tu reçois un code template et tu dois essayer de reproduire l'image dans `examples/<challenge>.png`. Tu gagnes des points pour chaque étape réussie ; ne passe pas trop de temps sur un seul défi si tu bloques. Et **n'essaye pas de reproduire l'image exactement!** Le comptage sera assez tolérant.
> Pour chaque défi, lance `dsd run <challenge>.fs` et écris directement dans le fichier `.fs`.
> Pour le scoring, une image sera générée à partir de ton shader (voir [make_screenshots.bat](./eval/make_screenshots.bat)). Tu ne peux pas utiliser de fichiers supplémentaires, mais tu es libre de faire tout le reste.

1. **Motif de labyrinthe** (8)

Tu te souviens de [ce motif](https://trixter.oldskool.org/2012/12/17/maze-generation-in-thirteen-bytes/#jp-carousel-839) à base de slash et backslash ? Peut-être l'un des plus vieux exemples d'[art génératif](https://www.reddit.com/r/generative/).
* (4) le motif est aléatoire
* (2) les lignes sont soit antialiasées (= floues, = pas d'arêtes dures), soit fortement pixelisées
* (1) utiliser une couleur différente pour les barres obliques et le fond, choisir une palette rétro
* (1) on peut se déplacer dans le motif avec la souris

2. **Ascii art** (6)

J'aime le *vieux* ASCII art, quand on remplaçait les pixels par des caractères petits ou grands. Mais une touche de couleur est sympa aussi.
* (4) faire de l'ASCII art
* (1) utiliser des caractères plus petits pour les zones plus sombres
* (1) colorer les symboles

3. **Motif de Bayer** (7)

Cette image a trop de couleurs et de bruit ! Elle serait plus jolie si elle était pixelisée. Si tu ne sais pas exactement ce que sont les motifs de Bayer, regarde le [wiki](https://en.wikipedia.org/wiki/Ordered_dithering).
* (1) rendre l'image monochrome
* (4) appliquer le motif de Bayer
* (1) rendre les pixels carrés
* (1) l'image n'est pas étirée (google « letterbox aspect ratio »)

4. **Un joli soleil** (5)

Tu te souviens de ces affiches ? Ça ne devrait pas être trop difficile avec un peu de maths.
> Exécute avec `dsd run 4_sun.fs -w 500 -h 888`
* (2) créer un soleil circulaire coloré
* (3) créer des rayons de soleil colorés

5. **Ensemble de Mandelbrot** (6)

Celui-ci a été réalisé sur du matériel très ancien, je me demande ce que ça donnera en haute résolution ? Voici le [wiki](https://en.wikipedia.org/wiki/Mandelbrot_set). L'idée est de répéter `z <- f(z)` et de détecter *quand* z diverge (par exemple `length(z) >= 2`).
> Exécute avec `dsd run "5_mandlebrot.fs" -w 888 -h 500`
* (4) l'ensemble est visible
* (1) appliquer des couleurs (plus clair quand z met plus de temps à diverger)
* (1) pouvoir zoomer autour de `u_zoomTarget`

6. **Étoiles** (8)

Faisons un de ces vieux économiseurs d'écran, ça fait longtemps que je n'en ai pas vu.
* (4) les étoiles brillent de façon douce
* (3) la lueur ne sature pas (on le voit bien si la lueur est grande)
* (1) taille et couleurs aléatoires par étoile

7. **Tapis de Sierpinski** (5)

Tant que tu y es, tu pourrais faire celui-là aussi ? C'est toujours agréable d'avoir un beau tapis.
* (4) le tapis est carré
* (1) lisser les bords

## Golfing (20)

Je n'ai plus beaucoup de ram, avant que je sature, peux-tu hacker mon cerveau pour faire un peu d'espace ?
> Le golfing consiste à rendre un code aussi petit que possible. C'est particulièrement facile avec les shaders puisqu'on utilise souvent des noms d'une seule lettre :p
> Tu as `a_golfing.fs`, tu peux tout y faire et le score dépendra du nombre de caractères restants. La sortie du shader ne doit pas trop changer !
* (0) points à 2500 caractères, le maximum (20) à 800 caractères ou moins
* Je retirerai des points pour chaque chose que je peux voir différente de l'original

## Jeu de la vie (20)

J'ai envie de jouer maintenant, tu pourrais me faire le [jeu de la vie](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) ? C'est assez simple à faire avec des shaders.
> Exécute avec `dsd run b_gameoflife.json -rte -w 500 -h 500`.
* (10) le jeu de la vie fonctionne
* (5) la vitesse de mise à jour est réglable avec un uniform
* (5) la grille boucle (le côté gauche = le côté droit)

## Snake ? (35)

Bon, j'avais oublié que le jeu de la vie est un jeu à 0 joueur, mais moi je suis 1. Tu pourrais faire Snake à la place ? C'est complètement possible de faire des jeux jouables aussi ! Mais c'est un peu plus dur...
> Cette fois, tu utiliseras des compute shaders en plus des fragment shaders. Ils s'exécutent sur le GPU mais ne servent pas à dessiner. Exécute avec `dsd run c_snake.json -rte`.
* (3) dessiner une grille
* (5) afficher le serpent
* (7) faire déplacer le serpent vers le curseur
* (5) faire apparaître un fruit, et le faire réapparaître quand il est mangé (ce n'est pas génant s'il se retrouve sur le serpent)
* (5) faire grandir le serpent quand il mange un fruit
* (5) réinitialiser le jeu quand le serpent se mord la queue
* (2) ajouter des yeux o_O
* (3) ajouter un vrai fond en dehors du jeu

## Path tracing (40)

Il est temps de faire des choses plus grandes. J'ai fait une sphère pour toi, peux-tu en faire un système solaire ? Tu devras peut-être combiner des effets 2D et 3D.
* (10) faire plusieurs planètes en orbite autour d'un soleil
* (5) colorer les planètes
* (5) faire briller le soleil
* (5) ajouter des étoiles en arrière-plan
* (5) ajouter une ceinture d'astéroïdes autour d'une planète
* (5) lisser l'image avec un [flou gaussien](https://en.wikipedia.org/wiki/Kernel_%28image_processing%29), tout dans le même shader
* (5) ajouter des anneaux pour les trajectoires des planètes

## Une ambiance rétro (30)

J'ai acheté un nouvel ordinateur récemment, mais il ne démarre pas… tu pourrais le faire fonctionner ?
> Exécute avec `dsd run e_retro.json`
* (10) reprendre n'importe lequel des shaders précédents et l'afficher à l'écran
* (5) ajouter des couleurs et un fond, libre à toi de modifier le fichier de scène (json)
* (12) appliquer des effets post-processing à l'écran, 4 points chacun : vignettage, aberration chromatique, parasites
* (3) appliquer un autre flou gaussien pour lisser l'image, mais dans `e_retro_gaussian.fs`

## Merci !

Merci d'avoir fait tout ça ! J'oublierai vite ton nom, mais assure-toi de te souvenir de moi : Noto🤖 le robot avec un super ordinateur qui fait tourner des jeux super cool !

### Tester & soumettre

N'oublie pas que les valeurs des uniforms sont sauvegardées *localement*! Soit sûr d'avoir copié les valeurs dans les .fs, tu peux utiliser le bouton "Copy" pour t'aider et tester avec le bouton "Reset".

Pour rendre tes résultats, `cd` dans `eval/` et lance `make_submit.ps1/.sh`. Dépose le `submit.zip` généré [ici](https://forms.gle/BN4zFAeHVdGD9Hbi6).
Rappelle-toi que tu ne dois pas avoir créé de nouveaux fichiers !

Pour voir exactement ce qui sera évalué, `cd` dans `eval/` et lance `make_screenshots.ps1/.sh`. Seules les premières secondes de chaque vidéo sont prises en compte.
