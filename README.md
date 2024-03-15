# ConwaysGameOfLife

Purpose:

I'm making this game to learn about Genetic Algorithms. 

Learnings:

 - Game of Life Implementation: This project deepened my understanding of Conway's Game of Life. What I thought was a genetic algorithm turned out to be a simple cellular automaton. I learned how to simulate cell state transitions and manage generations within a grid, gaining insights into the underlying rules involving the game.

 - Graphics Rendering in WinForms: I gained hands-on experience with graphics rendering in Windows Forms using GDI+. This involved tasks like drawing cells on a grid and updating the display to reflect changes in the game state, enhancing my proficiency in 2D rendering.

 - Optimizing Rendering Performance: I explored techniques like double buffering and minimizing redraws to enhance rendering performance. These optimizations proved effective in mitigating flickering and improving the overall visual quality of the application.


"The Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970.

It is a zero-player game,[2][3] meaning that its evolution is determined by its initial state, requiring no further input. One interacts with the Game of Life by creating an initial configuration and observing how it evolves. It is Turing complete and can simulate a universal constructor or any other Turing machine. 
The universe of the Game of Life is an infinite, two-dimensional orthogonal grid of square cells, each of which is in one of two possible states, live or dead (or populated and unpopulated, respectively). Every cell interacts with its eight neighbors, which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:

    Any live cell with fewer than two live neighbors dies, as if by underpopulation.
    Any live cell with two or three live neighbors lives on to the next generation.
    Any live cell with more than three live neighbors dies, as if by overpopulation.
    Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.

The initial pattern constitutes the seed of the system. The first generation is created by applying the above rules simultaneously to every cell in the seed, live or dead; births and deaths occur simultaneously, and the discrete moment at which this happens is sometimes called a tick.[nb 1] Each generation is a pure function of the preceding one. The rules continue to be applied repeatedly to create further generations. "
~Wikipedia Description
