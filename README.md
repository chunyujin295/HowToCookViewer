# HowToCook 菜谱查看器

[![MIT 许可证](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/aiursoftweb/howToCookViewer/blob/master/LICENSE)

HowToCook 菜谱查看器 - 菜谱可视化与网站

![截图](./screenshot.png)

> **Fork 版本说明**：本仓库基于 [aiursoftweb/howToCookViewer](https://github.com/aiursoftweb/howToCookViewer) 进行了以下改进：
>
> - ✅ **Open Graph (OG) 标签支持**：菜谱页面现在包含完整的 OG 和 Twitter Card 元标签，在 Notion、微信、QQ 等平台粘贴链接时会自动显示标题、描述和图片预览
> - ✅ **Docker 镜像兼容性**：Dockerfile 已修改为使用公开基础镜像（Microsoft dotnet、Node.js），不再依赖私有镜像仓库

默认用户名：`admin@default.com`，默认密码：`Admin@123456!`

## 在线体验

点击这里体验 [HowToCook 在线版](https://howtocook.aiursoft.com)

## 使用 Docker 部署（推荐）

首先，在 [这里](https://docs.docker.com/get-docker/) 安装 Docker。

### 使用自定义构建镜像（推荐）

```bash
# 克隆仓库
git clone https://github.com/chunyujin295/HowToCookViewer.git
cd HowToCookViewer

# 构建镜像
docker build -t howtocookviewer-custom .

# 运行容器
docker run -d --name howtocookviewer --restart unless-stopped -p 5000:5000 -v /var/www/howtocookviewer:/data howtocookviewer-custom
```

### 使用原始镜像

```bash
image=aiursoft/howtocookviewer
appName=howtocookviewer
sudo docker pull $image
sudo docker run -d --name $appName --restart unless-stopped -p 5000:5000 -v /var/www/$appName:/data $image
```

启动后，访问 `http://localhost:5000` 即可使用。

Docker 镜像配置：

| 属性       | 值                               |
|-----------|---------------------------------|
| 镜像名     | howtocookviewer-custom           |
| 端口       | 5000                            |
| 程序路径   | /app                             |
| 数据路径   | /data                            |
| 配置文件   | /data/appsettings.json           |

## 在 Ubuntu 上安装

以下脚本将在 Ubuntu 服务器上安装/更新应用，支持 Ubuntu 25.04。

在服务器上执行：

```bash
curl -sL https://github.com/aiursoftweb/howToCookViewer/raw/master/install.sh | sudo bash
```

指定自定义端口：

```bash
curl -sL https://github.com/aiursoftweb/howToCookViewer/raw/master/install.sh | sudo bash -s 8080
```

安装后会注册为 systemd 服务并自动启动。程序文件位于 `/opt/apps`，服务文件位于 `/etc/systemd/system`。

## 手动运行

运行要求：

1. 安装 [.NET 10 SDK](http://dot.net/) 和 [Node.js](https://nodejs.org/)
2. 在 `wwwroot` 目录执行 `npm install` 安装依赖
3. 执行 `dotnet run` 启动应用
4. 浏览器访问 [http://localhost:5000](http://localhost:5000)

## 使用 Visual Studio 运行

1. 打开项目中的 `.sln` 文件
2. 按 `F5` 运行

## 与上游版本的差异

| 功能                     | 上游版本 | 本 Fork |
|-------------------------|---------|---------|
| OG 标签（Notion/微信预览）| ❌      | ✅       |
| 公开 Docker 基础镜像      | ❌      | ✅       |
| npm 仓库                 | 私有（npm.aiursoft.com）| 公开（npmmirror）|

## 参与贡献

欢迎通过以下方式参与项目：

- 报告 Bug
- 提交 Pull Request
- 提交 Issue
- 提出功能建议

即使你有仓库的推送权限，也建议创建个人 Fork 并在功能分支上开发，保持主仓库整洁。

我们非常重视你对项目未来的反馈，可以通过 Issue Tracker 提交建议或功能请求。为了让反馈更有效，请尽可能提供详细信息。
