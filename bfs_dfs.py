from collections import deque


class bfs_cal:
    def bfs(graph, root):
        visitied = []
        queue = deque([root])
        while queue:
            node = queue.popleft()
            if node not in visitied:
                visitied.append(node)
                queue += graph[node] - set(visitied)
        return visitied
    
    
class dfs_cal:
    def dfs(graph, root):
        visitied = []
        stack = [root]
        while stack:
            node = stack.pop()
            if node not in visitied:
                visitied.append(node)
                stack += graph[node] - set(visitied)
                
        return visitied

graph_list = { 1: set([2, 3]),
                2: set([1, 3, 4]),
                3: set([1, 5]),
                4: set([1]),
                5: set([2,6]),
                6: set([3,4])}
root_node = 1

print(bfs_cal.bfs(graph_list,root_node))
print(dfs_cal.dfs(graph_list,root_node))
        