import random

parts = (
    'na', 'us', 'er', 'de', 'to', 'ta', 'vo', 'ca', 'ki', 'sh', 're', 'hu', 'ey', 'a', 'ou', 'iy', 're', 'oo', 'be',
    'vi', 'sa', 'ta', 'to', 'ba', 'i', 'u',
    'yo', 'va', 'bo', 'a', 'o', 'kt', 'dr', 'id', 'mo', 'ri', 'si', 'on', 'en', 'be', 'gw', 'wr', 'ia', 'b', 'wi',
    'se')
degree = ('Master', 'Candidate', 'Doctor')
title = ('Assistant professor', 'Professor')

def name_generator(seed):
    """value = ''
    for i in range(int(seed)):
        value = value + random.choice(parts)
    return value.title()"""
    return (''.join([str(random.choice(parts)) for i in range(seed)])).title()


def birth_generator():
    return str(random.randint(1945, 1990)) + '-' + str(random.randint(1, 12)) + '-' + str(random.randint(1, 28))


def degree_generator():
    return random.choice(degree)


def title_generator():
    return random.choice(title)


if __name__ == '__main__':
    names = []
    n = 100
    f = open('names.txt', 'w')
    for i in range(n):
        names.append({'name':name_generator(random.randint(2, 5))+' '+name_generator(random.randint(2, 7)),
                  'birth': birth_generator(), 'degree':degree_generator(), 'title': title_generator()})
        print(names[i])
        f.write(names[i]['name']+'\t'+names[i]['birth']+'\t'+names[i]['degree']+'\t'+names[i]['title']+'\t'+str(i+1)+'\n')
        #print(name_generator(random.randint(2, 5)), name_generator(random.randint(2, 7)), birth_generator(),
        #      degree_generator(), title_generator())

    f.close()
