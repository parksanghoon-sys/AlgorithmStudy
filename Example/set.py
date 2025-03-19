# 집합 함수들
s = {1, 2, 3}

s.add(4)  # {1, 2, 3, 4} - 요소 추가
s.remove(2)  # {1, 3, 4} - 요소 제거
# s.remove(5)  # KeyError - 없는 요소 제거시 에러
s.discard(5)  # 없는 요소 제거해도 에러 없음

# 집합 연산
s1 = {1, 2, 3}
s2 = {3, 4, 5}
union = s1.union(s2)  # {1, 2, 3, 4, 5} - 합집합
inter = s1.intersection(s2)  # {3} - 교집합
diff = s1.difference(s2)  # {1, 2} - 차집합
sym_diff = s1.symmetric_difference(s2)  # {1, 2, 4, 5} - 대칭 차집합

# 연산자로도 가능
union = s1 | s2  # 합집합
inter = s1 & s2  # 교집합
diff = s1 - s2   # 차집합
sym_diff = s1 ^ s2  # 대칭 차집합

# 사용 예제: 중복 제거
numbers = [1, 2, 2, 3, 3, 3, 4]
unique_numbers = list(set(numbers))  # [1, 2, 3, 4] - 중복 제거