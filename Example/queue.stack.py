from collections import deque

# 덱 생성 및 기본 연산
queue = deque([1, 2, 3])
queue.append(4)  # 오른쪽에 요소 추가: [1, 2, 3, 4]
queue.appendleft(0)  # 왼쪽에 요소 추가: [0, 1, 2, 3, 4]

right_item = queue.pop()  # 오른쪽 요소 제거: right_item = 4, queue = [0, 1, 2, 3]
left_item = queue.popleft()  # 왼쪽 요소 제거: left_item = 0, queue = [1, 2, 3]



# 사용 예제: BFS(너비 우선 탐색)
def bfs(graph, start):
    visited = set([start])
    queue = deque([start])
    result = []
    
    while queue:
        node = queue.popleft()
        result.append(node)
        
        for neighbor in graph[node]:
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
    
    return result

# 그래프 예시: {노드: [인접 노드 리스트]}
graph = {
    'A': ['B', 'C'],
    'B': ['A', 'D', 'E'],
    'C': ['A', 'F'],
    'D': ['B'],
    'E': ['B', 'F'],
    'F': ['C', 'E']
}

print(bfs(graph, 'A'))  # ['A', 'B', 'C', 'D', 'E', 'F'] - BFS 탐색 결과