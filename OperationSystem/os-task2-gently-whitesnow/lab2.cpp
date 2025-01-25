#include "lab2.h"
#include <cstring>
#include <fcntl.h>
#include <semaphore.h>
#include <unistd.h>

#define NUMBER_OF_THREADS 10

pthread_mutex_t cout_lock;
bool lock_initiated = false;

pthread_t tid[NUMBER_OF_THREADS];
int err = 0;
int a_thread = 0;
int b_thread = 1;
int c_thread = 2;
int d_thread = 3;
int e_thread = 4;
int f_thread = 5;
int g_thread = 6;
int h_thread = 7;
int i_thread = 8;
int k_thread = 9;

struct named_semaphore {
  sem_t *sem;
  char name[64];
};

// Семафоры синхронизации
named_semaphore semB, semC, semD;

// Семафоры завершения потоков
named_semaphore sem_a_thread_done;
named_semaphore sem_ab_thread_done;
named_semaphore sem_f_thread_done;
named_semaphore sem_hf_thread_done;

pid_t pid;

unsigned int lab2_thread_graph_id() { return 9; }

const char *lab2_unsynchronized_threads() { return "fhi"; }

const char *lab2_sequential_threads() { return "bcd"; }

void *thread_a(void *ptr);
void *thread_b(void *ptr);
void *thread_c(void *ptr);
void *thread_d(void *ptr);
void *thread_e(void *ptr);
void *thread_f(void *ptr);
void *thread_g(void *ptr);
void *thread_h(void *ptr);
void *thread_i(void *ptr);
void *thread_k(void *ptr);

void print_safe(const char *str) {
  pthread_mutex_lock(&cout_lock);
  std::cout << str << std::flush;
  pthread_mutex_unlock(&cout_lock);
}

void *thread_a(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("a");
    computation();
  }
  sem_post(sem_a_thread_done.sem);

  return ptr;
}

void *thread_b(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("b");
    computation();
  }

  sem_wait(sem_a_thread_done.sem);
  sem_post(sem_ab_thread_done.sem);

  for (int i = 0; i < 3; ++i) {
    sem_wait(semB.sem);
    print_safe("b");
    computation();
    sem_post(semC.sem);
  }

  pthread_join(tid[d_thread], NULL);

  return ptr;
}

void *thread_c(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("c");
    computation();
  }
  sem_wait(sem_ab_thread_done.sem);

  err = pthread_create(&tid[d_thread], NULL, thread_d, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread d. Error: " << strerror(err) << std::endl;
    return ptr;
  }

  for (int i = 0; i < 3; ++i) {
    sem_wait(semC.sem);

    print_safe("c");
    computation();
    sem_post(semD.sem);
  }

  pthread_join(tid[b_thread], NULL);

  err = pthread_create(&tid[e_thread], NULL, thread_e, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread e. Error: " << strerror(err) << std::endl;
    return ptr;
  }

  for (int i = 0; i < 3; ++i) {
    print_safe("c");
    computation();
  }

  pthread_join(tid[e_thread], NULL);

  err = pthread_create(&tid[h_thread], NULL, thread_h, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread h. Error: " << strerror(err) << std::endl;
    return ptr;
  }
  err = pthread_create(&tid[f_thread], NULL, thread_f, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread f. Error: " << strerror(err) << std::endl;
    return ptr;
  }
  err = pthread_create(&tid[i_thread], NULL, thread_i, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread i. Error: " << strerror(err) << std::endl;
    return ptr;
  }

  pthread_join(tid[i_thread], NULL);

  return ptr;
}

void *thread_d(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    sem_wait(semD.sem);
    print_safe("d");
    computation();
    sem_post(semB.sem);
  }
  return ptr;
}

void *thread_e(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("e");
    computation();
  }
  return ptr;
}

void *thread_f(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("f");
    computation();
  }
  sem_post(sem_f_thread_done.sem);
  return ptr;
}

void *thread_g(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("g");
    computation();
  }
  return ptr;
}

void *thread_h(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("h");
    computation();
  }
  sem_wait(sem_f_thread_done.sem);
  sem_post(sem_hf_thread_done.sem);
  for (int i = 0; i < 3; ++i) {
    print_safe("h");
    computation();
  }
  pthread_join(tid[g_thread], NULL);
  return ptr;
}

void *thread_i(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("i");
    computation();
  }
  sem_wait(sem_hf_thread_done.sem);
  err = pthread_create(&tid[g_thread], NULL, thread_g, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread g. Error: " << strerror(err) << std::endl;
    return ptr;
  }

  for (int i = 0; i < 3; ++i) {
    print_safe("i");
    computation();
  }
  pthread_join(tid[h_thread], NULL);

  err = pthread_create(&tid[k_thread], NULL, thread_k, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread k. Error: " << strerror(err) << std::endl;
    return ptr;
  }

  for (int i = 0; i < 3; ++i) {
    print_safe("i");
    computation();
  }
  pthread_join(tid[k_thread], NULL);

  return ptr;
}

void *thread_k(void *ptr) {
  for (int i = 0; i < 3; ++i) {
    print_safe("k");
    computation();
  }
  return ptr;
}

void clean_up() {
  if (semB.sem) {
    sem_close(semB.sem);
    sem_unlink(semB.name);
  }
  if (semC.sem) {
    sem_close(semC.sem);
    sem_unlink(semC.name);
  }
  if (semD.sem) {
    sem_close(semD.sem);
    sem_unlink(semD.name);
  }
  if (sem_a_thread_done.sem) {
    sem_close(sem_a_thread_done.sem);
    sem_unlink(sem_a_thread_done.name);
  }
  if (sem_ab_thread_done.sem) {
    sem_close(sem_ab_thread_done.sem);
    sem_unlink(sem_ab_thread_done.name);
  }
  if (sem_f_thread_done.sem) {
    sem_close(sem_f_thread_done.sem);
    sem_unlink(sem_f_thread_done.name);
  }
  if (sem_hf_thread_done.sem) {
    sem_close(sem_hf_thread_done.sem);
    sem_unlink(sem_hf_thread_done.name);
  }
  if (lock_initiated) {
    pthread_mutex_destroy(&cout_lock);
  }
}

bool has_error_sem_init(char name[64], named_semaphore &named_sem,
                        bool is_opened = false) {
  sprintf(named_sem.name, "/%s_%d", name, pid);
  sem_unlink(named_sem.name);
  named_sem.sem =
      sem_open(named_sem.name, O_CREAT | O_EXCL, 0644, is_opened ? 1 : 0);
  if (named_sem.sem == SEM_FAILED) {
    std::cerr << "Semaphore " << name << " init failed" << std::endl;
    return true;
  }
  return false;
}

int lab2_init() {
  pid = getpid();

  if (pthread_mutex_init(&cout_lock, NULL) != 0) {
    std::cerr << "Mutex init failed" << std::endl;
    return 1;
  }
  lock_initiated = true;
  pid_t pid = getpid();

  if (has_error_sem_init("b", semB) || has_error_sem_init("c", semC, true) ||
      has_error_sem_init("d", semD) ||
      has_error_sem_init("a_done", sem_a_thread_done) ||
      has_error_sem_init("ab_done", sem_ab_thread_done) ||
      has_error_sem_init("f_done", sem_f_thread_done) ||
      has_error_sem_init("hf_done", sem_hf_thread_done)) {
    clean_up();
    return 1;
  }

  err = pthread_create(&tid[a_thread], NULL, thread_a, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread a. Error: " << strerror(err) << std::endl;
    clean_up();
    return 1;
  }
  err = pthread_create(&tid[b_thread], NULL, thread_b, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread b. Error: " << strerror(err) << std::endl;

    pthread_cancel(tid[a_thread]);
    pthread_join(tid[a_thread], NULL);

    clean_up();
    return 1;
  }
  err = pthread_create(&tid[c_thread], NULL, thread_c, NULL);
  if (err != 0) {
    std::cerr << "Can't create thread c. Error: " << strerror(err) << std::endl;

    pthread_cancel(tid[a_thread]);
    pthread_join(tid[a_thread], NULL);

    pthread_cancel(tid[b_thread]);
    pthread_join(tid[b_thread], NULL);

    clean_up();
    return 1;
  }

  pthread_join(tid[c_thread], NULL);

  clean_up();

  std::cout << std::endl;
  return 0;
}
