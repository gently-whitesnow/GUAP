package info.servlet;

import info.model.BibliographicEntry;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

@WebServlet("/list")
public class ListServlet extends HttpServlet {
    private static final List<BibliographicEntry> entries = new ArrayList<>();

    public static List<BibliographicEntry> getEntries() {
        return entries;
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) 
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        PrintWriter out = response.getWriter();
        
        out.println("<!DOCTYPE html>");
        out.println("<html><head><title>Список литературы</title>");
        out.println("<style>");
        out.println("body { font-family: Arial, sans-serif; margin: 20px; }");
        out.println("table { border-collapse: collapse; width: 100%; }");
        out.println("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
        out.println("th { background-color: #f2f2f2; }");
        out.println("tr:nth-child(even) { background-color: #f9f9f9; }");
        out.println("form { display: inline; }");
        out.println("button { background: none; border: none; color: blue; text-decoration: underline; cursor: pointer; padding: 0; }");
        out.println("</style>");
        out.println("<script>");
        out.println("function confirmDelete(id) {");
        out.println("    if (confirm('Вы уверены, что хотите удалить эту запись?')) {");
        out.println("        document.getElementById('delete-form-' + id).submit();");
        out.println("    }");
        out.println("}");
        out.println("</script>");
        out.println("</head><body>");
        
        out.println("<h1>Список литературы</h1>");
        out.println("<a href='add.html'>Добавить запись</a><br><br>");
        
        out.println("<table>");
        out.println("<tr><th>Авторы</th><th>Название</th><th>Издательство</th><th>Год</th><th>Страниц</th><th>ISBN</th><th>Действия</th></tr>");
        
        for (BibliographicEntry entry : entries) {
            out.println("<tr>");
            out.println("<td>" + entry.getAuthors() + "</td>");
            out.println("<td>" + entry.getTitle() + "</td>");
            out.println("<td>" + entry.getPublisher() + "</td>");
            out.println("<td>" + entry.getYear() + "</td>");
            out.println("<td>" + entry.getPages() + "</td>");
            out.println("<td>" + entry.getIsbn() + "</td>");
            out.println("<td>");
            out.println("<form id='delete-form-" + entry.getId() + "' action='delete' method='post'>");
            out.println("<input type='hidden' name='id' value='" + entry.getId() + "'>");
            out.println("<button type='button' onclick='confirmDelete(" + entry.getId() + ")'>Удалить</button>");
            out.println("</form>");
            out.println("</td>");
            out.println("</tr>");
        }
        
        out.println("</table></body></html>");
    }
} 