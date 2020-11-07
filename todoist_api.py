from datetime import datetime, timezone, timedelta, date
import requests


projects_url = "https://api.todoist.com/rest/v1/projects"
tasks_url = "https://api.todoist.com/rest/v1/tasks"
dt = datetime.now(timezone(timedelta(hours=9)))
today_date = str(date(dt.year, dt.month, dt.day))


def get_today_tasks(token):
    tasks = []
    for task in _get_tasks(token):
        try:
            if task['due']['date'] == today_date:
                tasks.append(task['content'])
        except:
            pass
    return tasks


def get_today_belogings(token):
    response = requests.get(tasks_url, headers={"Authorization": "Bearer %s" % token}, params={"project_id": _get_belongig_id(token)})
    belogings = []
    if response.headers['Content-Type'] == 'application/json':
        for beloging in response.json():
            try:
                if beloging['due']['date'] == today_date:
                    belogings.append(beloging['content'])
            except:
                pass
        return belogings


def _get_belongig_id(token):
    for project in _get_projects(token):
        if project['name'] == '持ち物':
            return project['id']


def _get_projects(token):
    response = requests.get(projects_url, headers={"Authorization": "Bearer %s" % token})
    if response.headers['Content-Type'] == 'application/json':
        return response.json()


def _get_tasks(token):
    response = requests.get(tasks_url, headers={"Authorization": "Bearer %s" % token})
    if response.headers['Content-Type'] == 'application/json':
        return response.json()


if __name__ == "__main__":
    import sys
    args = sys.argv
    token = args[1]
    print("今日のタスク")
    print(get_today_tasks(token))
    print("今日の持ち物")
    print(get_today_belogings(token))
