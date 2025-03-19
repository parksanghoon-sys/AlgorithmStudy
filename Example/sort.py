# 정렬 함수들
numbers = [3, 1, 4, 2]
sorted_numbers = sorted(numbers)  # 원본은 유지하고 정렬된 새 리스트 반환
print(numbers)         # [3, 1, 4, 2] - 원본 유지
print(sorted_numbers)  # [1, 2, 3, 4] - 정렬된 새 리스트

numbers.sort()  # 원본 리스트 정렬
print(numbers)  # [1, 2, 3, 4] - 원본이 변경됨

# 내림차순 정렬
numbers.sort(reverse=True)
print(numbers)  # [4, 3, 2, 1]

# 사용자 정의 정렬
people = [{'name': 'Kim', 'age': 30}, {'name': 'Lee', 'age': 25}, {'name': 'Park', 'age': 40}]
people.sort(key=lambda x: x['age'])  # 나이 기준으로 정렬
print(people)  # [{'name': 'Lee', 'age': 25}, {'name': 'Kim', 'age': 30}, {'name': 'Park', 'age': 40}]

# 다중 기준 정렬
students = [('Kim', 'A', 95), ('Lee', 'c', 99), ('Park', 'A', 90)]
students.sort(key=lambda x: (x[1],-x[2]))  # 점수 내림차순, 학점 오름차순 앞에 조건이 우선순위
print(students)  # [('Kim', 'A', 95), ('Lee', 'B', 95), ('Park', 'A', 90)]