#include "lab3.h"
#include <iostream>
#include <string>
#include <windows.h>

unsigned int lab3_thread_graph_id() { return 5; }

const char *lab3_unsynchronized_threads() { return "bceg"; }

const char *lab3_sequential_threads() { return "ghk"; }

CRITICAL_SECTION g_coutLock;

HANDLE semG, semH, semK;
HANDLE sem_g_done, sem_c_done, sem_m_done, sem_k_done;

HANDLE a_thread;
HANDLE b_thread;
HANDLE c_thread;
HANDLE d_thread;
HANDLE e_thread;
HANDLE f_thread;
HANDLE g_thread;
HANDLE h_thread;
HANDLE i_thread;
HANDLE k_thread;
HANDLE n_thread;
HANDLE m_thread;

void print_safe(const char *str) {
  EnterCriticalSection(&g_coutLock);
  std::cout << str << std::flush;
  LeaveCriticalSection(&g_coutLock);
}

DWORD WINAPI thread_n(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("n");
    computation();
  }
  return 0;
}

DWORD WINAPI thread_i(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("i");
    computation();
  }
  return 0;
}

DWORD WINAPI thread_m(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("m");
    computation();
  }
  ReleaseSemaphore(sem_m_done, 1, NULL);
  WaitForSingleObject(sem_k_done, INFINITE);

  for (int i = 0; i < 3; ++i) {
    print_safe("m");
    computation();
  }

  WaitForSingleObject(n_thread, INFINITE);

  return 0;
}

DWORD WINAPI thread_k(LPVOID) {
  // выполнение синхронного участка ghk
  for (int i = 0; i < 3; ++i) {
    WaitForSingleObject(semK, INFINITE);
    print_safe("k");
    computation();
    ReleaseSemaphore(semG, 1, NULL);
  }

  // дожидаемся завершения потоко "h" и "g"
  WaitForSingleObject(h_thread, INFINITE);
  WaitForSingleObject(g_thread, INFINITE);

  // создаем дочерние потоки
  i_thread = CreateThread(NULL, 0, thread_i, NULL, 0, NULL);
  if (i_thread == NULL) {
    std::cerr << "CreateThread_i error: " << GetLastError() << std::endl;
    return 1;
  }

  m_thread = CreateThread(NULL, 0, thread_m, NULL, 0, NULL);
  if (m_thread == NULL) {
    std::cerr << "CreateThread_m error: " << GetLastError() << std::endl;
    return 1;
  }

  for (int i = 0; i < 3; ++i) {
    print_safe("k");
    computation();
  }

  // дожидаемся потока "i"
  WaitForSingleObject(i_thread, INFINITE);
  // синхронизируемся с потоком "m"
  WaitForSingleObject(sem_m_done, INFINITE);

  // создаем последний дочерний поток
  n_thread = CreateThread(NULL, 0, thread_n, NULL, 0, NULL);
  if (n_thread == NULL) {
    std::cerr << "CreateThread_n error: " << GetLastError() << std::endl;
    return 1;
  }
  // синхронизируемся с потоком "m"
  ReleaseSemaphore(sem_k_done, 1, NULL);

  // дожидаемся завершения потока "m", который в свою очередь дождется
  // завершения "n"
  WaitForSingleObject(m_thread, INFINITE);

  // выход
  return 0;
}

DWORD WINAPI thread_h(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    WaitForSingleObject(semH, INFINITE);
    print_safe("h");
    computation();
    ReleaseSemaphore(semK, 1, NULL);
  }

  return 0;
}

DWORD WINAPI thread_f(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("f");
    computation();
  }

  return 0;
}

DWORD WINAPI thread_d(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("d");
    computation();
  }

  WaitForSingleObject(f_thread, INFINITE);

  return 0;
}

DWORD WINAPI thread_b(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("b");
    computation();
  }
  WaitForSingleObject(e_thread, INFINITE);

  return 0;
}

DWORD WINAPI thread_e(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("e");
    computation();
  }
  return 0;
}

DWORD WINAPI thread_g(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("g");
    computation();
  }
  ReleaseSemaphore(sem_g_done, 1, NULL);
  WaitForSingleObject(sem_c_done, INFINITE);
  for (int i = 0; i < 3; ++i) {
    print_safe("g");
    computation();
  }
  ReleaseSemaphore(sem_g_done, 1, NULL);
  WaitForSingleObject(sem_c_done, INFINITE);

  for (int i = 0; i < 3; ++i) {
    WaitForSingleObject(semG, INFINITE);
    print_safe("g");
    computation();
    ReleaseSemaphore(semH, 1, NULL);
  }

  return 0;
}

DWORD WINAPI thread_a(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("a");
    computation();
  }

  return 0;
}

DWORD WINAPI thread_c(LPVOID) {
  for (int i = 0; i < 3; ++i) {
    print_safe("c");
    computation();
  }
  // дожидаемся дочерний поток
  WaitForSingleObject(a_thread, INFINITE);

  // создаем 3 дочерних потока
  b_thread = CreateThread(NULL, 0, thread_b, NULL, 0, NULL);
  if (b_thread == NULL) {
    std::cerr << "CreateThread_b error: " << GetLastError() << std::endl;
    return 1;
  }
  e_thread = CreateThread(NULL, 0, thread_e, NULL, 0, NULL);
  if (e_thread == NULL) {
    std::cerr << "CreateThread_e error: " << GetLastError() << std::endl;
    return 1;
  }
  g_thread = CreateThread(NULL, 0, thread_g, NULL, 0, NULL);
  if (g_thread == NULL) {
    std::cerr << "CreateThread_g error: " << GetLastError() << std::endl;
    return 1;
  }

  for (int i = 0; i < 3; ++i) {
    print_safe("c");
    computation();
  }

  // дожидаемся завершение дочернего потока "b", который дожидается "e"
  WaitForSingleObject(b_thread, INFINITE);
  // синхронизируемся с потоком "g"
  WaitForSingleObject(sem_g_done, INFINITE);

  // создаем дочерние потоки
  d_thread = CreateThread(NULL, 0, thread_d, NULL, 0, NULL);
  if (d_thread == NULL) {
    std::cerr << "CreateThread_d error: " << GetLastError() << std::endl;
    return 1;
  }

  f_thread = CreateThread(NULL, 0, thread_f, NULL, 0, NULL);
  if (f_thread == NULL) {
    std::cerr << "CreateThread_f error: " << GetLastError() << std::endl;
    return 1;
  }

  // синхронизируемся с потоком "с"
  ReleaseSemaphore(sem_c_done, 1, NULL);

  for (int i = 0; i < 3; ++i) {
    print_safe("c");
    computation();
  }

  // дожидаемся потока "d", который в свою очередь дождется "f"
  WaitForSingleObject(d_thread, INFINITE);
  // синхронизируемся с потоком "g"
  WaitForSingleObject(sem_g_done, INFINITE);

  // создаем поток "h"
  h_thread = CreateThread(NULL, 0, thread_h, NULL, 0, NULL);
  if (h_thread == NULL) {
    std::cerr << "CreateThread_h error: " << GetLastError() << std::endl;
    return 1;
  }

  // создаем новый основной поток "k"
  k_thread = CreateThread(NULL, 0, thread_k, NULL, 0, NULL);
  if (k_thread == NULL) {
    std::cerr << "CreateThread_k error: " << GetLastError() << std::endl;
    return 1;
  }

  // синхронизируемся с потоком "c"
  ReleaseSemaphore(sem_c_done, 1, NULL);

  // дожидаемся основной поток "k"
  WaitForSingleObject(k_thread, INFINITE);

  return 0;
}

void clean_up() {
  if (semG != NULL) {
    CloseHandle(semG);
  }
  if (semH != NULL) {
    CloseHandle(semH);
  }
  if (semK != NULL) {
    CloseHandle(semK);
  }
  if (sem_g_done != NULL) {
    CloseHandle(sem_g_done);
  }
  if (sem_c_done != NULL) {
    CloseHandle(sem_c_done);
  }
  if (sem_m_done != NULL) {
    CloseHandle(sem_m_done);
  }
  if (sem_k_done != NULL) {
    CloseHandle(sem_k_done);
  }

  DeleteCriticalSection(&g_coutLock);

  if (a_thread != NULL) {
    WaitForSingleObject(a_thread, INFINITE);
    CloseHandle(a_thread);
  }
  if (b_thread != NULL) {
    WaitForSingleObject(b_thread, INFINITE);
    CloseHandle(b_thread);
  }
  if (c_thread != NULL) {
    WaitForSingleObject(c_thread, INFINITE);
    CloseHandle(c_thread);
  }
  if (d_thread != NULL) {
    WaitForSingleObject(d_thread, INFINITE);
    CloseHandle(d_thread);
  }
  if (e_thread != NULL) {
    WaitForSingleObject(e_thread, INFINITE);
    CloseHandle(e_thread);
  }
  if (f_thread != NULL) {
    WaitForSingleObject(f_thread, INFINITE);
    CloseHandle(f_thread);
  }
  if (g_thread != NULL) {
    WaitForSingleObject(g_thread, INFINITE);
    CloseHandle(g_thread);
  }
  if (h_thread != NULL) {
    WaitForSingleObject(h_thread, INFINITE);
    CloseHandle(h_thread);
  }
  if (i_thread != NULL) {
    WaitForSingleObject(i_thread, INFINITE);
    CloseHandle(i_thread);
  }
  if (k_thread != NULL) {
    WaitForSingleObject(k_thread, INFINITE);
    CloseHandle(k_thread);
  }
  if (n_thread != NULL) {
    WaitForSingleObject(n_thread, INFINITE);
    CloseHandle(n_thread);
  }
  if (m_thread != NULL) {
    WaitForSingleObject(m_thread, INFINITE);
    CloseHandle(m_thread);
  }
}

int lab3_init() {
  InitializeCriticalSection(&g_coutLock);

  semG = CreateSemaphore(NULL, 1, 1, NULL);
  semH = CreateSemaphore(NULL, 0, 1, NULL);
  semK = CreateSemaphore(NULL, 0, 1, NULL);
  sem_g_done = CreateSemaphore(NULL, 0, 1, NULL);
  sem_c_done = CreateSemaphore(NULL, 0, 1, NULL);
  sem_m_done = CreateSemaphore(NULL, 0, 1, NULL);
  sem_k_done = CreateSemaphore(NULL, 0, 1, NULL);

  if (semG == NULL || semH == NULL || semK == NULL || sem_g_done == NULL ||
      sem_c_done == NULL || sem_m_done == NULL || sem_k_done == NULL) {
    std::cerr << "CreateSemaphore error: " << GetLastError() << std::endl;
    clean_up();
    return 1;
  }

  // дочерний поток
  a_thread = CreateThread(NULL, 0, thread_a, NULL, 0, NULL);
  if (a_thread == NULL) {
    std::cerr << "CreateThread_a error: " << GetLastError() << std::endl;
    clean_up();
    return 1;
  }

  // основной поток
  c_thread = CreateThread(NULL, 0, thread_c, NULL, 0, NULL);
  if (c_thread == NULL) {
    std::cerr << "CreateThread_c error: " << GetLastError() << std::endl;
    clean_up();
    return 1;
  }

  // ждем завершения основного потока
  WaitForSingleObject(c_thread, INFINITE);

  clean_up();
  std::cout << std::endl;
  return 0;
}
