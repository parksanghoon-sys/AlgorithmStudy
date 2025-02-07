def average(lst):
  """리스트의 평균을 반환합니다."""
  if not lst:  # 빈 리스트인 경우 0 반환
    return 0
  return sum(lst) / len(lst)

total_cout = int(input())

grade_list = list(map(int, input().split()))

max_grade = max(grade_list)

avg_list = []
for i in range(len(grade_list)):
    avg_list.append(grade_list[i] / max_grade * 100)

print(average(avg_list))