#  **************************************************************************  #
#                                                                              #
#                                                       :::    :::    :::      #
#    Problem Number: 9506                              :+:    :+:      :+:     #
#                                                     +:+    +:+        +:+    #
#    By: tkdgns0220 <boj.kr/u/tkdgns0220>            +#+    +#+          +#+   #
#                                                   +#+      +#+        +#+    #
#    https://boj.kr/9506                           #+#        #+#      #+#     #
#    Solved: 2025/04/30 05:28:44 by tkdgns0220    ###          ###   ##.kr     #
#                                                                              #
#  **************************************************************************  #
import sys

while True:
    num = int(input()) 
    list = []
    if num == -1:
        break
    
    for i in range(1, num):
        if num % i == 0:
            list.append(i)
    
    num_sum = sum(list)
    if num_sum == num:
        print(num, "=", end= " ")
        print(*list, sep=" + ")
    else:
        print("%d is NOT perfect." %num)
        