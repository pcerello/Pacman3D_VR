from bs4 import BeautifulSoup
import csv
import os

for etage in range(1, 7):

    with open(f'./stages_html/Etage {etage}.html', 'r') as f:
        html_content = f.read()
    f.close()

    from bs4 import BeautifulSoup
    import csv

    def rotate_matrix_left(matrix):
        # Step 1: Transpose the matrix
        transposed_matrix = [list(row) for row in zip(*matrix)]
        
        # Step 2: Reverse the order of the columns
        rotated_matrix = [row[::-1] for row in transposed_matrix]
        
        return rotated_matrix

    # Parse the HTML content
    soup = BeautifulSoup(html_content, 'html.parser')

    # Extract the grid data
    grid_data = []
    data_saved = False
    for row in soup.select('.waffle tbody tr'):
        if (not(data_saved)):
            data_saved = True
        else:
            row_data = []
            for cell in row.select('td'):
                color_class = cell.get('class', [''])[0]
                if color_class == 's0':
                    row_data.append('W')
                elif color_class == 's1':
                    row_data.append('Y')
                elif color_class == 's2':
                    row_data.append('O')
                elif color_class == 's3':
                    row_data.append('C')
                elif color_class == 's4':
                    row_data.append('G')
                elif color_class == 's5':
                    row_data.append('N')
                elif color_class == 's6':
                    row_data.append('P')
                elif color_class == 's7':
                    row_data.append('R')
                elif color_class == 's8':
                    row_data.append('???')
                else:
                    row_data.append('W')
            grid_data.append(row_data)

    # Transpose the grid data
    transposed_grid = list(map(list, zip(*grid_data)))
    # Rotate the grid data 90 degrees to the left
    transposed_grid = rotate_matrix_left(transposed_grid)
    # Mirror the grid data horizontally
    transposed_grid = [row[::-1] for row in transposed_grid]


    # Write the transposed grid data to a CSV file
    with open(f'./stages/etage{etage}.csv', 'w', newline='') as csvfile:
        csv_writer = csv.writer(csvfile)
        csv_writer.writerows(transposed_grid)

    print(f"CSV file 'etage{etage}.csv' has been created.")

    import pandas as pd
    import numpy as np

    df = pd.read_csv(f'./stages/etage{etage}.csv', header=None)
    print(df)