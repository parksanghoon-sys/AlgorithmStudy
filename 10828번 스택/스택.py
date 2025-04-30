#  **************************************************************************  #
#                                                                              #
#                                                       :::    :::    :::      #
#    Problem Number: 10828                             :+:    :+:      :+:     #
#                                                     +:+    +:+        +:+    #
#    By: tkdgns0220 <boj.kr/u/tkdgns0220>            +#+    +#+          +#+   #
#                                                   +#+      +#+        +#+    #
#    https://boj.kr/10828                          #+#        #+#      #+#     #
#    Solved: 2025/04/30 05:37:56 by tkdgns0220    ###          ###   ##.kr     #
#                                                                              #
#  **************************************************************************  #

import sys

class Stack:
    def __init__(self):
        self.stack = []
        pass
    
    def push(self, num):
        self.stack.append(num)
        
    def pop(self):
        if self.empty():
            return -1        
        return self.stack.pop()
    
    def size(self):
        return len(self.stack)
    
    def empty(self):
        if len(self.stack) == 0:
            return 1
        else:
            return 0
        
    def top(self):
        if  len(self.stack) == 0:
            return -1
        else:
            return self.stack[-1]

stack = Stack()

n = int(sys.stdin.readline())

for _ in range(n):
    cmd = sys.stdin.readline().split()
    
    if cmd[0] == 'push':
        stack.push(int(cmd[1]))
    elif cmd[0] == 'top':
        print(stack.top())
    elif cmd[0] == 'size':
        print(stack.size())
    elif cmd[0] == 'pop':
        print(stack.pop())
    elif cmd[0] == 'empty':
        print(stack.empty())