# 딕셔너리 함수들
person = {'name': 'Kim', 'age': 30}

name = person.get('name')  # 'Kim' - 키 조회
salary = person.get('salary', 0)  # 0 - 키가 없으면 기본값 반환
# print(name, salary)  # Kim 0


all_keys = list(person.keys())
all_values= list(person.values())
all_item = list(person.items())


# for key in all_keys:
#     print(key, person[key]) # name Kim, age 30
    
# for key, value in person.items():
#     print(key, value)  # name Kim, age 30

# 사용 예제: 단어 카운팅
text = "hello world hello"
word_count = {}
for word in text.split():
    if word in word_count:
        word_count[word] += 1
    else:
        word_count[word] = 1
## print(word_count)  # {'hello': 2, 'world': 1}

# get() 활용한 더 간결한 방법
word_count = {}
for word in text.split():
    word_count[word] = word_count.get(word, 0) + 1
print(word_count)  # {'hello': 2, 'world': 1}