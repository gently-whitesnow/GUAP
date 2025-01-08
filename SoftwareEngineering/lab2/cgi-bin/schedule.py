#!/usr/bin/env python3
import os
import json
import sys
import urllib.parse
import uuid

# Путь к файлу с данными
DATA_FILE = "bus_schedule.json"

# Загрузка данных из файла
def load_schedule():
    if os.path.exists(DATA_FILE):
        with open(DATA_FILE, "r") as file:
            return json.load(file)
    return []  # Если файл отсутствует, возвращаем пустой список

# Сохранение данных в файл
def save_schedule(schedule):
    with open(DATA_FILE, "w") as file:
        json.dump(schedule, file)

def print_headers():
    print("Access-Control-Allow-Origin: *")
    print("Access-Control-Allow-Methods: GET, POST, DELETE, OPTIONS")
    print("Access-Control-Allow-Headers: Content-Type")
    print("Content-Type: application/json")
    print()

def parse_post_data():
    content_length = int(os.environ.get('CONTENT_LENGTH', 0))
    post_data = sys.stdin.read(content_length)
    return urllib.parse.parse_qs(post_data)

def handle_get():
    schedule = load_schedule()
    print_headers()
    print(json.dumps(schedule))

def handle_post():
    data = parse_post_data()
    action = data.get("action", [""])[0]
    schedule = load_schedule()

    if action == "delete":
        handle_delete(data, schedule)
    else:
        new_bus = {
            "id": str(uuid.uuid4()),
            "number": data.get("number", [""])[0],
            "route": data.get("route", [""])[0],
            "departure": data.get("departure", [""])[0]
        }
        schedule.append(new_bus)
        save_schedule(schedule)
        print_headers()
        print(json.dumps({"message": "Bus added successfully"}))

def handle_delete(data, schedule):
    id = data.get("id", [""])[0]
    updated_schedule = [bus for bus in schedule if bus["id"] != id]
    save_schedule(updated_schedule)
    print_headers()
    print(json.dumps({"message": f"Bus {id} deleted successfully"}))

if os.environ['REQUEST_METHOD'] == 'OPTIONS':
    print_headers()
elif os.environ['REQUEST_METHOD'] == 'GET':
    handle_get()
elif os.environ['REQUEST_METHOD'] == 'POST':
    handle_post()
