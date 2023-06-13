import { observer } from "mobx-react-lite";
import { Interweave } from "interweave";
import { useState, useEffect, useCallback } from "react";
import TestModule from "../TestModule/TestModule";
import { useStore } from "../../../store";

const MarkdownHandler = (props) => {
  const [articleContent, setArticleContent] = useState(null);

  const { articleStore } = useStore();
  const { article } = articleStore;

  useEffect(() => {
    setArticleContent(null);
    fillArticleContent();
  }, [article.fileURL]);
  
  
  const fillArticleContent = () => {
    if(article.fileURL === undefined){
      return;
    }
    
    readFileAndFillContent(article.fileURL, (mdFileString) => {
      let htmlFileString = convertToHtmlString(mdFileString);
      let domElement = convertToDomElementsWithTestsApplying(htmlFileString);
      
      setArticleContent(domElement);
    });
  };

  const readFileAndFillContent = (path, callback) => {
    
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", path, false);
    rawFile.onreadystatechange = function () {
      if (rawFile.readyState === 4) {
        if (rawFile.status === 200 || rawFile.status == 0) {
          callback(rawFile.responseText);
        }
      }
    };
    rawFile.send(null);
  };
  
  const convertToHtmlString = (rawText) => {
    var md = require("markdown-it")({
      html: true,
      linkify: true,
      typographer: true,
    }).use(require("markdown-it-emoji"));

    return md.render(rawText);
  };
  

  // !test

  // +=  позитивный
  // -= негативный
  // +=  позитивный
  // -= негативный

  // !test

  const convertToDomElementsWithTestsApplying = (htmlFileString) => {
    let domElements = [];

    const testKey = "!test";

    let firstTestModuleIndex = htmlFileString.indexOf(testKey);

    let testModuleCounterId = 0;
    while (firstTestModuleIndex !== -1) {
      let lastTestModuleIndex = htmlFileString
          .substring(firstTestModuleIndex + 1, htmlFileString.length)
          .indexOf(testKey) + firstTestModuleIndex + testKey.length + 1;

      let testModulesRawString = htmlFileString.substring(
        firstTestModuleIndex, lastTestModuleIndex);

      domElements.push(
        <Interweave
          content={htmlFileString.substring(0, firstTestModuleIndex)}
        />
      );

      domElements.push(applyTestModules(testModulesRawString, testModuleCounterId++));

      htmlFileString = htmlFileString.substring(
        lastTestModuleIndex,htmlFileString.length);

      firstTestModuleIndex = htmlFileString.indexOf(testKey);
    }

    domElements.push(<Interweave content={htmlFileString} />);
    return (
      <>
        {domElements.map((element) => {
          return element;
        })}
      </>
    );
  };

  const applyTestModules = (partHtml, testModuleCounterId) => {
    const rightAnswerKey = "+"; // +=
    const wrongAnswerKey = "-"; // +=
    const stoppowers = ["+=", "-=", "<", "!test"];

    let rightAnswers = [];
    let wrongAnswers = [];

    let isPositiveChoice = false;

    partHtml = partHtml.replaceAll('\n', '');

    // -1 for checking i+1 condition
    for (let i = 0; i < partHtml.length - 1; i++) {
      if (partHtml.charAt(i + 1) !== "=") {
        continue;
      }

      if (partHtml.charAt(i) === rightAnswerKey) {
        isPositiveChoice = true;
      } else if (partHtml.charAt(i) === wrongAnswerKey) {
        isPositiveChoice = false;
      } else {
        continue;
      }
      
      // +2 (+=, -=)
      let lastChar = 0;
      let buffer = "";
      for (var j = i + 2; j < partHtml.length; j++) {
        if (
          // look stoppowers length
          stoppowers.includes(partHtml.substring(j, j + 1)) ||
          stoppowers.includes(partHtml.substring(j, j + 2)) ||
          stoppowers.includes(partHtml.substring(j, j + 5))
        ) {
          lastChar = j;
          break;
        }
        buffer += partHtml.charAt(j);
      }
      i = lastChar -1;
      if (isPositiveChoice) {
        rightAnswers.push(buffer);
      } else {
        wrongAnswers.push(buffer);
      }
    }

    return <TestModule rightAnswers={rightAnswers} wrongAnswers={wrongAnswers} color={props.color} testModuleCounterId={testModuleCounterId}/>;
  }

  return articleContent;
};

export default observer(MarkdownHandler);
