# 리스트 조작 함수들
numbers = [1, 2, 3]
numbers.append(4)      # [1, 2, 3, 4] - 끝에 요소 추가
numbers.insert(0, 0)   # [0, 1, 2, 3, 4] - 특정 위치에 삽입
popped = numbers.pop() # popped = 4, numbers = [0, 1, 2, 3] - 마지막 요소 제거 및 반환
numbers.pop(0)         # numbers = [1, 2, 3] - 인덱스 0 위치의 요소 제거
numbers.remove(2)      # numbers = [1, 3] - 값이 2인 요소 제거

# 사용 예제: 스택 구현
stack = []
stack.append(1)  # 스택에 1 추가
stack.append(2)  # 스택에 2 추가
top_item = stack.pop()  # top_item = 2, 마지막에 추가된 요소 제거
print(stack)  # [1]