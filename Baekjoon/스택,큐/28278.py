import sys

n = int(sys.stdin.readline())
st = []
for i in range(n):
    cmd =list(map(int,sys.stdin.readline().split()))
    if cmd[0] == 1:
        st.append(int(cmd[-1]))
    elif cmd[0] == 2:
        if st:
            print(st.pop(-1))
            continue
        print(-1)
    elif cmd[0] == 3:
        print(len(st))
    elif cmd[0] == 4:
        if st:
            print(0)
        else:
            print(1)
    elif cmd[0] == 5 :
        if st :
            print(st[-1])
        else:
            print(-1)