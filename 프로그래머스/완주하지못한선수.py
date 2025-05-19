import sys

def solution(participant, completion):
    result = ""
    participant.sort()
    completion.sort()

    for i in range(len(completion)) :

        if(participant[i] != completion[i]):
            return  participant[i]            
    
                
    return participant[-1]


participant = ["mislav", "stanko", "mislav", "ana"]
completion = ["stanko", "ana", "mislav"]

print(solution(participant,completion))



    